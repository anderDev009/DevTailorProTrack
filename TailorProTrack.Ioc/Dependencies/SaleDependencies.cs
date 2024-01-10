
using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class SaleDependencies
    {
        public static void AddSaleDependencies(this IServiceCollection service)
        {
            //repositorios
            service.AddScoped<ISalesRepository, SalesRepository>();

            //servicios
            service.AddTransient<ISaleService, SaleService>();
        }
    }
}
