using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HmIpMonitor.Contracts;
using HmIpMonitor.EntityFramework;
using HmIpMonitor.EntityFramework.Models;
using Samhammer.DependencyInjection.Attributes;

namespace HmIpMonitor.Logic
{
    [Inject()]
    public class DashboardLogic : IDashboardLogic
    {
        private readonly HmIpMonitorContext _context;
        private readonly IDeviceLogic _deviceLogic;

        public DashboardLogic(HmIpMonitorContext context, IDeviceLogic deviceLogic)
        {
            _context = context;
            _deviceLogic = deviceLogic;
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

        public List<Dashboard> LoadAll()
        {
            return _context.Dashboards.ToList();
        }

        public void Delete(long id)
        {
            _context.Dashboards.Remove(_context.Dashboards.First(x => x.Id == id));
            _context.SaveChanges();
        }

        public Dashboard Load(long id)
        {
            return _context.Dashboards.First(x => x.Id == id);
        }

        public List<CcuValueDto> GetAllValues(long id)
        {
            var dashboard = _context.Dashboards.First(x => x.Id == id);
            var deviceIds = dashboard.DashboardDeviceParameters.Select(x => x.DeviceParameter.DeviceId).ToList();
            var parameterIds = dashboard.DashboardDeviceParameters.Select(x => x.DeviceParameterId).ToList();
            return _deviceLogic.LoadValues(deviceIds, parameterIds);
        }
    }
}