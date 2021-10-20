using System;
using System.Diagnostics;
using System.Threading;

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
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);

                }
                catch (Exception e)
                {
                    Debug.WriteLine("error during run of update service: " + e);
                }
            }
        }
    }
}