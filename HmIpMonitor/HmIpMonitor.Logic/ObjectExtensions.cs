using System.Linq;
using System.Reflection;

namespace HmIpMonitor.Logic
{
    public static class ObjectExtensions
    {
        public static TOut PopulateFrom<TOut, TIn>(this TOut @this, TIn from)
        {
            var properties = @this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty |
                                                           BindingFlags.GetProperty);
            var fromProperties = from.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty |
                                                              BindingFlags.GetProperty);
            properties.ToList().ForEach(p =>
            {
                var fromProperty = fromProperties.FirstOrDefault(x => x.Name == p.Name);
                if (fromProperty == null)
                {
                    return;
                }
                p.SetValue(@this, fromProperty.GetValue(from));
            });

            return @this;
        }
    }
}