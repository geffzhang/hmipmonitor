using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HmIpMonitor.EntityFramework.Models
{
    [Table("Dashboard")]
    public class Dashboard : BaseEntity
    {
        [MaxLength(255)]
        public string Title { get; set; }

        public virtual List<DashboardDeviceParameter> DashboardDeviceParameters { get; set; } =
            new List<DashboardDeviceParameter>();
    }
}