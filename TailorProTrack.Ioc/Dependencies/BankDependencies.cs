

using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class BankDependencies
    {
        public static void AddBankDependencies(this IServiceCollection services) 
        {
            services.AddScoped<IBankRepository,BankRepository>();
            services.AddScoped<IBankAccountRepository,BankAccountRepository>();

            services.AddTransient<IBankService,BankService>();
            services.AddTransient<IBankAccountService,BankAccountService>();
        }
    }
}
