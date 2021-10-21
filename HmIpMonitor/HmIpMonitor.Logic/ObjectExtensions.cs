using System.Linq;
using System.Reflection;

namespace HmIpMonitor.Logic
{
    public static class ObjectExtensions
    {
        public static T PopulateFrom<T>(this T @this, T from)
        {
            var type = @this.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty |
                               BindingFlags.GetProperty);
            properties.ToList().ForEach(p =>
            {
                p.SetValue(@this, p.GetValue(from));
            });

            return @this;
        }
    }
}