using System.Collections.Generic;

namespace HmIpMonitor.Models
{
    public class CreateEditDashboardDeviceModel
    {
        public string Title { get; set; }

        public List<CreateEditDashboardDeviceParameterModel> Parameters { get; set; } =
            new List<CreateEditDashboardDeviceParameterModel>();
    }
}