using System.ComponentModel.DataAnnotations.Schema;

namespace HmIpMonitor.EntityFramework.Models
{
    [Table("DashboardDeviceParameter")]
    public class DashboardDeviceParameter : BaseEntity
    {
        public long DashboardId { get; set; }
        public virtual Dashboard Dashboard { get; set; }

        public long DeviceParameterId { get; set; }
        public virtual DeviceParameter DeviceParameter { get; set; }
    }
}