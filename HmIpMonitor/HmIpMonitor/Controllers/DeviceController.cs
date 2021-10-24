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
                    DeviceParameter = d.DeviceParameter,
                    Title = deviceData.Title
                };
            });
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            DeviceParameter dp = new DeviceParameter();
            dp.DeviceId = collection["DeviceId"];
            dp.Channel = collection["Channel"];
            dp.Parameter = collection["Parameter"];
            dp.ValueErrorThreshold = double.TryParse(collection["ValueErrorThreshold"], out var errorThreshold) ? errorThreshold : 0;
            dp.ValueWarnThreshold = double.TryParse(collection["ValueWarnThreshold"], out var warnThreshold) ? warnThreshold : 0;
            dp.ValueThresholdDirectionRight = bool.TryParse(collection["ValueThresholdDirectionRight"], out var direction) ? direction : true;
            _deviceLogic.SaveOrUpdateDevice(dp);
            return RedirectToAction("Index");
        }
    }
}
