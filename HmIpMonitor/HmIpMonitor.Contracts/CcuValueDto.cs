using HmIpMonitor.Contracts.CcuApi;
using HmIpMonitor.EntityFramework.Models;

namespace HmIpMonitor.Contracts
{
    public class CcuValueDto : CcuValue
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }

        public DeviceParameter DeviceParameter { get; set; }
    }
}