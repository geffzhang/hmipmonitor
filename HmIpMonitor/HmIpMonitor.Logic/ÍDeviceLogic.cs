using System.Collections.Generic;
using HmIpMonitor.Contracts;
using HmIpMonitor.EntityFramework.Models;

namespace HmIpMonitor.Logic
{
    public interface IDeviceLogic
    {
        HmIpDevice SaveOrUpdateDevice(DeviceParameter parameter);
        List<HmIpDevice> GetAll();
        List<CcuValueDto> GetAllValues();
        CcuDeviceDto GetDeviceData(string deviceId);
        List<CcuValueDto> LoadValues(List<string> deviceIds, List<long> parameterIds);
    }
}