using System.Collections.Generic;
using System.Linq;
using HmIpMonitor.ControllerModels;
using HmIpMonitor.Logic;
using HmIpMonitor.Models;
using Microsoft.AspNetCore.Mvc;

namespace HmIpMonitor.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        [HttpPost]
        public bool SaveOrUpdate(ClientDeviceModel device)
        {
            new DeviceLogic().SaveOrUpdateDevice(device);
            return true;
        }

        [HttpGet]
        public List<HmIpDevice> GetAll()
        {
            return new DeviceLogic().GetAll();
        }

        [HttpGet]
        public List<CcuValueDto> GetAllValues()
        {
            return new DeviceLogic().GetAllValues().Where(x => x != null).ToList();
        }
    }
}