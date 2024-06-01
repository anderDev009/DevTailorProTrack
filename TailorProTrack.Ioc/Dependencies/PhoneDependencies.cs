using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class PhoneDependencies
    {
        public static void AddPhoneDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPhoneService,PhoneService>();
            services.AddScoped<IPhoneRepository,PhoneRepository>();
        }
    }
}
