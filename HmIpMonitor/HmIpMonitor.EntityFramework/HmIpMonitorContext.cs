using HmIpMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace HmIpMonitor.EntityFramework
{
    public class HmIpMonitorContext : DbContext
    {
        public HmIpMonitorContext(DbContextOptions<HmIpMonitorContext> options): base(options){}

        public DbSet<HmIpDevice> Devices { get; set; }
        public DbSet<DeviceParameter> DeviceParameters { get; set; }
        public DbSet<CcuDataPoint> DataPoints { get; set; }
    }
}