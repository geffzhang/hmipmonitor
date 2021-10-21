namespace HmIpMonitor.Models
{
    public class CcuValueDto
    {
        public long Ts { get; set; }
        public double V { get; set; }
        public double S { get; set; }

        // transient
        public string Id { get; set; }
        public string Name { get; set; }
        public long DeviceParameterId { get; set; }
    }
}