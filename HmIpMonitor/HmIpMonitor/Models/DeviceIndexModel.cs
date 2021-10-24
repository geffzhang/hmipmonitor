using System.Collections.Generic;
using HmIpMonitor.EntityFramework.Models;

namespace HmIpMonitor.Models
{
    public class DeviceIndexModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public List<DeviceParameter> DeviceParameter { get; set; }
    }
}