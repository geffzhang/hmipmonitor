using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HmIpMonitor.Contracts;
using HmIpMonitor.EntityFramework.Models;
using HmIpMonitor.Logic;
using HmIpMonitor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

        public IActionResult Index(long id = 0)
        {
            if (id == 0)
            {
                return View(_dashboardLogic.LoadAll());
            }
            else
            {
                return RedirectToAction("Details", new { Id = id });
            }
        }

        [HttpGet]
        public IActionResult Create(long id = 0)
        {
            return View(GetDashboardModel(id));
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            return View(id);
        }

        [HttpGet]
        public PartialViewResult CarPartial(long id)
        {
            return new PartialViewResult
            {
                ViewName = "_DashboardDetailsPartial",
                ViewData = new ViewDataDictionary<List<CcuValueDto>>(ViewData, _dashboardLogic.GetAllValues(id))
            };
        }

        [HttpPost]
        public IActionResult Create(CreateEditDashboardModel model)
        {
            _dashboardLogic.SaveOrUpdate(model.Id, model.Title, model.DeviceParameters.Where(x => x.Active).Select(x => x.Id).ToList());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            _dashboardLogic.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            return RedirectToAction("Create", new { Id = id });
        }

        private CreateEditDashboardModel GetDashboardModel(long id = 0)
        {
            CreateEditDashboardModel dashboard = new();
            Dashboard dbDashboard = null;

            if (id > 0)
            {
                dashboard.Id = id;
                dbDashboard = _dashboardLogic.Load(id);
                dashboard.Title = dbDashboard.Title;
            }

            var parameters = _deviceLogic.GetAll().SelectMany(d =>
            {
                var deviceName = _deviceLogic.GetDeviceData(d.Id).Title;
                return d.DeviceParameter.Select(dp =>
                {
                    return new CreateEditDashboardDeviceParameterModel
                    {
                        Title = deviceName,
                        Parameter = dp.Parameter,
                        Active = dbDashboard?.DashboardDeviceParameters.Any(x => x.DeviceParameterId == dp.Id) ?? false,
                        Id = dp.Id
                    };
                });
            }).ToList();

            dashboard.DeviceParameters = parameters;
            return dashboard;
        }
    }
}
