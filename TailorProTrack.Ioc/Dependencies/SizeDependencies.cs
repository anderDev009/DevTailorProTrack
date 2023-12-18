using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;


namespace TailorProTrack.Ioc.Dependencies
{
    public static class SizeDependencies
    {
        public static void AddSizeDependencies(this IServiceCollection service)
        {
            service.AddScoped<ISizeRepository, SizeRepository>();
            service.AddTransient<ISizeService, SizeService>(); 
        }
    }
}
