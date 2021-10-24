using System.ComponentModel.DataAnnotations.Schema;

namespace HmIpMonitor.EntityFramework.Models
{
    [Table("DataPoint")]
    public class CcuDataPoint : BaseEntity
    {
        public virtual DeviceParameter DeviceParameter { get; set; }
        public long DeviceParameterId { get; set; }

        public long Timestamp { get; set; }
        public double Value { get; set; }
        public double Quality { get; set; }
    }
}