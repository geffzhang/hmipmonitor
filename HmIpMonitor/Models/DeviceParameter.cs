using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HmIpMonitor.Models
{
    [Table("DeviceParameter")]
    public class DeviceParameter
    {
        public long Id { get; set; }
        public virtual HmIpDevice Device { get; set; }
        [MaxLength(255)]
        public string DeviceId { get; set; }
        [MaxLength(255)]
        public string Channel { get; set; }
        [MaxLength(255)]
        public string Parameter { get; set; }

        public virtual List<CcuDataPoint> DataPoints { get; set; }
    }
}