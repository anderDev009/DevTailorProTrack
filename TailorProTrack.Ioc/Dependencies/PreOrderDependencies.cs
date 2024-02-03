
using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class PreOrderDependencies
    {
        public static void AddPreOrderDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPreOrderRepository, PreOrderRepository>();
            services.AddScoped<IPreOrderProductsRepository,PreOrderProductRepository>();

            services.AddTransient<IPreOrderService, PreOrderService>();
            services.AddTransient<IPreOrderProductService, PreOrderProductsService>();
        }
    }
}
