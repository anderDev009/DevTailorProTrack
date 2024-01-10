using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;


namespace TailorProTrack.Ioc.Dependencies
{
    public static class PaymentDependencies
    {
        public static void AddPaymentDependencies(this IServiceCollection services)
        {
            //repositorios
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();

            //servicios
            services.AddTransient<IPaymentService,PaymentService>();
            services.AddTransient<IPaymentTypeService,PaymentTypeService>();

        }
    }
}
