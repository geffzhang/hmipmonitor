using System.Collections.Generic;
using HmIpMonitor.Contracts;
using HmIpMonitor.Models;

namespace HmIpMonitor.Logic
{
    public interface IDeviceLogic
    {
        HmIpDevice SaveOrUpdateDevice(ClientDeviceModel device);
        List<HmIpDevice> GetAll();
        List<CcuValueDto> GetAllValues();
        CcuDeviceDto GetDeviceData(string deviceId);
    }
}