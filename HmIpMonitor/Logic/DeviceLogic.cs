using System.Collections.Concurrent;

using System.Collections.Generic;
using System.Linq;
using HmIpMonitor.ControllerModels;
using HmIpMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace HmIpMonitor.Logic
{
    public class DeviceLogic
    {
        private static ConcurrentDictionary<string, CcuDeviceDto> deviceCache =
            new();

        public HmIpDevice SaveOrUpdateDevice(ClientDeviceModel device)
        {
            using var ctx = new HmIpMonitorContext();
            var dbDevice = ctx.Devices.FirstOrDefault(x => x.Id == device.Id);

            if (dbDevice == null)
            {
                dbDevice = new HmIpDevice();
                dbDevice.Id = device.Id;
                ctx.Devices.Add(dbDevice);
                ctx.SaveChanges();
            }

            var dbParameter =
                ctx.DeviceParameters.FirstOrDefault(x => x.DeviceId == device.Id && x.Parameter == device.Parameter && x.Channel == device.Channel);

            if (dbParameter == null)
            {
                dbParameter = new DeviceParameter();
                dbParameter.Channel = device.Channel;
                dbParameter.DeviceId = dbDevice.Id;
                dbParameter.Parameter = device.Parameter;
                ctx.DeviceParameters.Add(dbParameter);
                ctx.SaveChanges();
            }
            
            return ctx.Devices.Include(x => x.DeviceParameter).FirstOrDefault(x => x.Id == device.Id);
        }

        public List<HmIpDevice> GetAll()
        {
            using var ctx = new HmIpMonitorContext();
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
                    }

                    return ccuData;
                });
            }).ToList();
        }

        //public void AddDataPoint(string deviceId, string value)

        private void InitDeviceCache(List<string> deviceIds)
        {
            // concurrentdict is used though we use lock() here because dotnet-dump can show contents of concurrent dict easily

            lock (deviceCache)
            {
                if (deviceIds != null && deviceCache.Count == deviceIds.Count)
                {
                    return;
                }

                deviceCache.Clear();

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
                var data = deviceCache[deviceId];
                if (data == null)
                {
                    InitDeviceCache(null);
                }

                return deviceCache[deviceId];
            }
        }
    }
}