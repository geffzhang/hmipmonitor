using System.Collections.Generic;
using System.Linq;
using HmIpMonitor.EntityFramework;
using HmIpMonitor.EntityFramework.Models;
using Samhammer.DependencyInjection.Attributes;

namespace HmIpMonitor.Logic
{
    [Inject()]
    public class DashboardLogic : IDashboardLogic
    {
        private readonly HmIpMonitorContext _context;

        public DashboardLogic(HmIpMonitorContext context)
        {
            _context = context;
        }

        public Dashboard SaveOrUpdate(long id, string title, List<long> parameterIds)
        {
            Dashboard dashboard;

            if (id == 0)
            {
                dashboard = new Dashboard();
                _context.Dashboards.Add(dashboard);
            }
            else
            {
                dashboard = _context.Dashboards.First(x => x.Id == id);
            }

            dashboard.Title = title;
            _context.SaveChanges();

            dashboard.DashboardDeviceParameters.Clear();
            _context.SaveChanges();

            _context.DashboardDeviceParameters.AddRange(parameterIds.Select(p => new DashboardDeviceParameter
            {
                DashboardId = dashboard.Id,
                DeviceParameterId = p
            }));

            _context.SaveChanges();
            return _context.Dashboards.First(x => x.Id == dashboard.Id);
        }
    }
}