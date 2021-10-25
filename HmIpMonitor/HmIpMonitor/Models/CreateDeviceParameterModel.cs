using HmIpMonitor.EntityFramework.Models;

namespace HmIpMonitor.Models
{
    public class CreateDeviceParameterModel : DeviceParameter
    {
        public string DeviceName { get; set; }
    }
}