using System.Collections.Concurrent;

using System.Collections.Generic;
using System.Linq;
using HmIpMonitor.Contracts;
using HmIpMonitor.EntityFramework;
using HmIpMonitor.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Samhammer.DependencyInjection.Attributes;

namespace HmIpMonitor.Logic
{
    [Inject()]
    public class DeviceLogic : IDeviceLogic
    {
        private readonly ICcuApi _ccuApi;
        private readonly HmIpMonitorContext ctx;

        private static ConcurrentDictionary<string, CcuDeviceDto> deviceCache =
            new();

        public DeviceLogic(ICcuApi ccuApi, HmIpMonitorContext ctx)
        {
            _ccuApi = ccuApi;
            this.ctx = ctx;
        }

        public HmIpDevice SaveOrUpdateDevice(DeviceParameter parameter)
        {
            var dbDevice = ctx.Devices.FirstOrDefault(x => x.Id == parameter.DeviceId);

            if (dbDevice == null)
            {
                dbDevice = new HmIpDevice();
                dbDevice.Id = parameter.DeviceId;
                ctx.Devices.Add(dbDevice);
                ctx.SaveChanges();
            }

            var dbParameter =
                ctx.DeviceParameters.FirstOrDefault(x => x.DeviceId == parameter.DeviceId && x.Parameter == parameter.Parameter && x.Channel == parameter.Channel);

            if (dbParameter == null)
            {
                dbParameter = new DeviceParameter();
                dbParameter.Channel = parameter.Channel;
                dbParameter.DeviceId = dbDevice.Id;
                dbParameter.Parameter = parameter.Parameter;
                dbParameter.ValueErrorThreshold = parameter.ValueErrorThreshold;
                dbParameter.ValueWarnThreshold = parameter.ValueWarnThreshold;
                dbParameter.ValueThresholdDirectionRight = parameter.ValueThresholdDirectionRight;
                ctx.DeviceParameters.Add(dbParameter);
                ctx.SaveChanges();
            }
            else
            {
                dbParameter.ValueErrorThreshold = parameter.ValueErrorThreshold;
                dbParameter.ValueWarnThreshold = parameter.ValueWarnThreshold;
                dbParameter.ValueThresholdDirectionRight = parameter.ValueThresholdDirectionRight;
                ctx.SaveChanges();
            }

            InitDeviceCache();
            
            return ctx.Devices.Include(x => x.DeviceParameter).FirstOrDefault(x => x.Id == parameter.DeviceId);
        }

        public List<HmIpDevice> GetAll()
        {
            return ctx.Devices.Include(x => x.DeviceParameter).ToList();
        }

        public List<CcuValueDto> GetAllValues()
        {
            var devices = GetAll();
            InitDeviceCache(devices.Select(x => x.Id).ToList());
            var ccuApi = new CcuApi();
            return devices.SelectMany(d =>
            {
                return d.DeviceParameter.Select(dp =>
                {
                    var ccuData = ccuApi.GetDataFor(dp.DeviceId, dp.Channel, dp.Parameter);

                    if (ccuData != null)
                    {
                        ccuData.Id = d.Id;
                        ccuData.Name = GetDeviceData(d.Id)?.Title;
                        ccuData.DeviceParameterId = dp.Id;
                    }

                    return ccuData;
                });
            }).Where(x => x != null).ToList();
        }

        //public void AddDataPoint(string deviceId, string value)

        private void InitDeviceCache(List<string> deviceIds = null)
        {
            // concurrentdict is used though we use lock() here because dotnet-dump can show contents of concurrent dict easily

            lock (deviceCache)
            {
                if (deviceIds != null && deviceCache.Count == deviceIds.Count)
                {
                    return;
                }

                deviceCache.Clear();
                if (deviceIds == null)
                {
                    deviceIds = GetAll().Select(x => x.Id).ToList();
                }

                var ccuApi = new CcuApi();
                deviceIds.ForEach(d =>
                {
                    var ccuData = ccuApi.GetDeviceDataFor(d);
                    deviceCache[d] = ccuData ?? new CcuDeviceDto { Title = "unknown" };
                });
            }
        }

        public CcuDeviceDto GetDeviceData(string deviceId)
        {
            lock (deviceCache)
            {
                var data = deviceCache.TryGetValue(deviceId, out var fromCache) ? fromCache : null;
                if (data == null)
                {
                    InitDeviceCache(null);
                }

                return deviceCache[deviceId];
            }
        }
    }
}