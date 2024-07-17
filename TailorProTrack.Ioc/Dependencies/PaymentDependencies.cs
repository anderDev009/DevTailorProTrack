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
            services.AddBankDependencies();
            services.AddExpensesDependencies();
            //dependencias
            //repositorios
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddTransient<IPaymentExpensesRepository, PaymentExpensesRepositoryRepository>();
            services.AddTransient<INoteCreditRepository, NoteCreditRepository>();

            //servicios
            services.AddTransient<IPaymentService,PaymentService>();
            services.AddTransient<IPaymentTypeService,PaymentTypeService>();
            services.AddTransient<IPaymentExpensesService, PaymentExpenseService>();
            services.AddTransient<INoteCreditService, NoteCreditService>();
        }
    }
}
