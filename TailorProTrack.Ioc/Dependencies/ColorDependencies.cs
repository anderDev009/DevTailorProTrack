using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts.Color;
using TailorProTrack.Application.Service;
using TailorProTrack.Application.Service.Filter;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class ColorDependencies
    {
        public static void AddColorDependencies(this IServiceCollection service)
        {
            service.AddScoped<IColorRepository, ColorRepository>();

            service.AddTransient<IColorService, ColorService>();
            service.AddTransient<IColorFilterService, ColorFilterService>();
        }
    }
}
