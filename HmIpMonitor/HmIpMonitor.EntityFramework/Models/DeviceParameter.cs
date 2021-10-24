using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HmIpMonitor.EntityFramework.Models
{
    [Table("DeviceParameter")]
    public class DeviceParameter : BaseEntity
    {
        public virtual HmIpDevice Device { get; set; }
        [MaxLength(255)]
        public string DeviceId { get; set; }
        [MaxLength(255)]
        public string Channel { get; set; }
        [MaxLength(255)]
        public string Parameter { get; set; }

        // TODO get threshold from configured temperature of thermostat
        public double ValueWarnThreshold { get; set; }
        public double ValueErrorThreshold { get; set; }
        // x < ValueWarnThreshold < ValueErrorThreshold vs. ValueErrorThreshold < ValueWarnThreshold < x
        public bool ValueThresholdDirectionRight { get; set; } = true;

        public virtual List<CcuDataPoint> DataPoints { get; set; }
    }
}