namespace HmIpMonitor.Models
{
    public class CcuDeviceDto
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Id => Address;
    }
}