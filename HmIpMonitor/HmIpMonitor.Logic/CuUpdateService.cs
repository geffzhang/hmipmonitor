using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HmIpMonitor.EntityFramework;
using HmIpMonitor.EntityFramework.Models;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace HmIpMonitor.Logic
{
    [Inject]
    public class CcuUpdateService : ICcuUpdateService
    {
        private readonly HmIpMonitorContext _context;
        private readonly IDeviceLogic _deviceLogic;

        private static Dictionary<long, long> oldTimestampCache = new();

        public static void Run(IServiceProvider serviceProvider)
        {
            var t = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        using var scope = serviceProvider.CreateScope();
                        var ccuUpdateService = scope.ServiceProvider.GetService<ICcuUpdateService>();
                        ccuUpdateService.Execute();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("error during run of update service: " + e);
                    }
                }
            });

            t.Start();
        }

        public CcuUpdateService(HmIpMonitorContext context, IDeviceLogic deviceLogic)
        {
            _context = context;
            _deviceLogic = deviceLogic;
        }

        public void Execute()
        {
            var deviceData = _deviceLogic.GetAllValues();
            
            deviceData.ForEach(d =>
            {
                if (oldTimestampCache.TryGetValue(d.DeviceParameter.Id, out var oldTs) && oldTs == d.Ts)
                {
                    // only update bigger changes
                    return;
                }
                _context.DataPoints.Add(new CcuDataPoint
                {
                    DeviceParameterId = d.DeviceParameter.Id,
                    Quality = d.S,
                    Timestamp = d.Ts,
                    Value = d.V
                });
                oldTimestampCache[d.DeviceParameter.Id] = d.Ts;
            });
            _context.SaveChanges();
        }
    }
}