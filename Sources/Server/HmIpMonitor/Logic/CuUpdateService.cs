using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HmIpMonitor.Models;

namespace HmIpMonitor.Logic
{
    public class CcuUpdateService
    {
        public static void Run()
        {
            var t = new Thread(() =>
            {
                new CcuUpdateService().Execute();
            });
            t.Start();
        }

        public void Execute()
        {
            var oldTimestampCache = new Dictionary<long, long>();

            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    var deviceData = new DeviceLogic().GetAllValues();

                    using var ctx = new HmIpMonitorContext();
                    deviceData.ForEach(d =>
                    {
                        if (oldTimestampCache.TryGetValue(d.DeviceParameterId, out var oldTs) && oldTs == d.Ts)
                        {
                            // only update bigger changes
                            return;
                        }
                        ctx.DataPoints.Add(new CcuDataPoint
                        {
                            DeviceParameterId = d.DeviceParameterId,
                            Quality = d.S,
                            Timestamp = d.Ts,
                            Value = d.V
                        });
                        oldTimestampCache[d.DeviceParameterId] = d.Ts;
                    });
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("error during run of update service: " + e);
                }
            }
        }
    }
}