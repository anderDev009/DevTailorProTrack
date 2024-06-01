
using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Service;
using TailorProTrack.Application.Service.Filter;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class ClientDependencies
    {
        public static void AddClientDependencies(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository,ClientRepository>();
            services.AddTransient<IClientService,ClientService>();
            services.AddTransient<IClientFilterService,ClientFilterService>();
        }
    }
}
