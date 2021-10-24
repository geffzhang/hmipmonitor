using HmIpMonitor.Contracts.CcuApi;

namespace HmIpMonitor.Contracts
{
    public class CcuDeviceDto : CcuDevice
    {
        public string Id => Address;
    }
}