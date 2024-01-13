

using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class CategoryDependencies
    {
        public static void AddCategoryDependencies(this IServiceCollection service)
        {
            
            service.AddScoped<ICategorySizeRepository, CategorySizeRepository>();

            //servicios
            service.AddTransient<ICategorySizeService, CategorySizeService>();
        }
    }
}
