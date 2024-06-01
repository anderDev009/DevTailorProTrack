using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class TypeProductDependencies
    {
        public static void AddTypeProductDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITypeProdRepository, TypeProdRepository>();
            services.AddTransient<ITypeProdService, TypeProdService>();
        }
    }
}
