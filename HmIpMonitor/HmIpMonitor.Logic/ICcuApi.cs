using HmIpMonitor.Contracts;

namespace HmIpMonitor.Logic
{
    public interface ICcuApi
    {
        CcuValueDto GetDataFor(string deviceId, string channel, string parameter);
        CcuDeviceDto GetDeviceDataFor(string deviceId);
    }
}