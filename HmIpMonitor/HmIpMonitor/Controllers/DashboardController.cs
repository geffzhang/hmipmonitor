using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HmIpMonitor.Logic;
using HmIpMonitor.Models;
using Microsoft.AspNetCore.Http;

namespace HmIpMonitor.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDeviceLogic _deviceLogic;
        private readonly IDashboardLogic _dashboardLogic;

        public DashboardController(IDeviceLogic deviceLogic, IDashboardLogic dashboardLogic)
        {
            _deviceLogic = deviceLogic;
            _dashboardLogic = dashboardLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(GetDashboardModel());
        }

        [HttpPost]
        public IActionResult Create(CreateEditDashboardModel model)
        {
            _dashboardLogic.SaveOrUpdate(model.Id, model.Title, model.DeviceParameters.Select(x => x.Id).ToList());
            return View(GetDashboardModel());
        }

        private CreateEditDashboardModel GetDashboardModel()
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
            return dashboard;
        }
    }
}
