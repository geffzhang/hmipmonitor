using System.Collections.Generic;
using HmIpMonitor.EntityFramework.Models;

namespace HmIpMonitor.Logic
{
    public interface IDashboardLogic
    {
        Dashboard SaveOrUpdate(long id, string title, List<long> parameterIds);
    }
}