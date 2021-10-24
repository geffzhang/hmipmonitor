using HmIpMonitor.Contracts.CcuApi;

namespace HmIpMonitor.Contracts
{
    public class CcuValueDto : CcuValue
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string ParameterName { get; set; }
        public long DeviceParameterId { get; set; }
    }
}