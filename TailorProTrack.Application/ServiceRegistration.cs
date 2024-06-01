
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TailorProTrack.Application
{
    public static class ServiceRegistration
    {
        public static void AddServicesApplicationLayer(this IServiceCollection services)
        {
            #region configurando automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion
        }
    }
}
