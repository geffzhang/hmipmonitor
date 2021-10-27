using System.Collections.Generic;

namespace HmIpMonitor.Models
{
    public class CreateEditDashboardModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        // contains all deviceparameters from database, ordered by deviceid
        public List<CreateEditDashboardDeviceModel> DeviceParameters { get; set; }
    }
}