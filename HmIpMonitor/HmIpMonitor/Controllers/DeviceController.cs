using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HmIpMonitor.Contracts;
using HmIpMonitor.EntityFramework.Models;
using HmIpMonitor.Logic;
using HmIpMonitor.Models;
using Microsoft.AspNetCore.Http;

namespace HmIpMonitor.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceLogic _deviceLogic;

        public DeviceController(IDeviceLogic deviceLogic)
        {
            _deviceLogic = deviceLogic;
        }

        public IActionResult Index()
        {
            var model = _deviceLogic.GetAll().Select(d =>
            {
                var deviceData = _deviceLogic.GetDeviceData(d.Id);
                return new DeviceIndexModel
                {
                    Address = deviceData.Address,
                    DeviceParameter = d.DeviceParameter.OrderBy(x => x.Parameter).ToList(),
                    Title = deviceData.Title
                };
            });
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            if (id > 0)
            {
                DeviceParameter dp = _deviceLogic.GetDeviceParameter(id);
                var deviceData = _deviceLogic.GetDeviceData(dp.DeviceId);
                var model = new CreateDeviceParameterModel().PopulateFrom(dp);
                model.DeviceName = deviceData.Title;
                return View(model);
            }

            return View(new CreateDeviceParameterModel());
        }

        [HttpPost]
        public ActionResult Create(CreateDeviceParameterModel data)
        {
            DeviceParameter dp = new DeviceParameter();
            dp.DeviceId = data.DeviceId;
            dp.Channel = data.Channel;
            dp.Parameter = data.Parameter;
            dp.ValueErrorThreshold = data.ValueErrorThreshold;
            dp.ValueWarnThreshold = data.ValueWarnThreshold;
            dp.ValueThresholdDirectionRight = data.ValueThresholdDirectionRight;
            _deviceLogic.SaveOrUpdateDevice(dp);
            return RedirectToAction("Index");
        }
    }
}
