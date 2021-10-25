using System.Collections.Generic;
using HmIpMonitor.Contracts;
using HmIpMonitor.EntityFramework.Models;

namespace HmIpMonitor.Logic
{
    public interface IDashboardLogic
    {
        Dashboard SaveOrUpdate(long id, string title, List<long> parameterIds);
        List<Dashboard> LoadAll();
        void Delete(long id);
        Dashboard Load(long id);
        List<CcuValueDto> GetAllValues(long id);
    }
}