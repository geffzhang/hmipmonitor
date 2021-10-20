using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HmIpMonitor.Models
{
    [Table("HmIpDevice")]
    public class HmIpDevice
    {
        [MaxLength(255)]
        public string Id { get; set; }

        public virtual List<DeviceParameter> DeviceParameter { get; set; }
    }
}