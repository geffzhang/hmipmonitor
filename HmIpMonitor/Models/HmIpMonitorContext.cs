using Microsoft.EntityFrameworkCore;

namespace HmIpMonitor.Models
{
    public class HmIpMonitorContext : DbContext
    {
        public DbSet<HmIpDevice> Devices { get; set; }
        public DbSet<DeviceParameter> DeviceParameters { get; set; }
        public DbSet<CcuDataPoint> DataPoints { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=192.168.2.149;Database=hmipmonitor;User Id=hmipmonitor;Password=hmipmonitor;Port=5432");
        }
    }
}