using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts.Size;
using TailorProTrack.Application.Service;
using TailorProTrack.Application.Service.Filter;
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
            service.AddTransient<ISizeFilterService, SizeFilterService>();
        }
    }
}
