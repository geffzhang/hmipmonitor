using System.Collections.Generic;

namespace HmIpMonitor.Models
{
    public class DeviceIndexModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public List<DeviceParameter> DeviceParameter { get; set; }
    }
}