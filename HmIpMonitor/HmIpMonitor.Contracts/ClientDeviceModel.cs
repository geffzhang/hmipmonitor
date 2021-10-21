namespace HmIpMonitor.Contracts
{
    public class ClientDeviceModel
    {
        public string Id { get; set; }
        public string Channel { get; set; }
        public string Parameter { get; set; }
        public double ValueWarnThreshold { get; set; }
        public double ValueErrorThreshold { get; set; }
        public bool ValueThresholdDirectionRight { get; set; }
    }
}