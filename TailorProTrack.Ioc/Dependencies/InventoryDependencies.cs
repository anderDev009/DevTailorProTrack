using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class InventoryDependencies
    {
        public static void AddInventoryDependencies(this IServiceCollection service)
        {
            service.AddScoped<IInventoryRepository, InventoryRepository>();

            service.AddScoped<IInventorySizeRepository, InventorySizeRepository>();

            service.AddTransient<IInventoryService, InventoryService>();
        }
    }
}
