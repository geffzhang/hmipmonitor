using HmIpMonitor.Contracts;
using HmIpMonitor.Contracts.CcuApi;

namespace HmIpMonitor.Logic
{
    public interface ICcuApi
    {
        CcuValue GetDataFor(string deviceId, string channel, string parameter);
        CcuDevice GetDeviceDataFor(string deviceId);
    }
}