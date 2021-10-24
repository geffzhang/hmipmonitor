using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HmIpMonitor.Logic;
using HmIpMonitor.Models;

namespace HmIpMonitor.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDeviceLogic _deviceLogic;

        public DashboardController(IDeviceLogic deviceLogic)
        {
            _deviceLogic = deviceLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var dashboard = new CreateEditDashboardModel();

            var parameters = _deviceLogic.GetAll().SelectMany(d =>
            {
                var deviceName = _deviceLogic.GetDeviceData(d.Id).Title;
                return d.DeviceParameter.Select(dp =>
                {
                    return new CreateEditDashboardDeviceParameterModel
                    {
                        Title = deviceName,
                        Parameter = dp.Parameter,
                        Active = false,
                        Id = dp.Id
                    };
                });
            }).ToList();

            dashboard.DeviceParameters = parameters;

            return View(dashboard);
        }
    }
}
