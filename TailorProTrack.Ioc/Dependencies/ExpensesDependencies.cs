
using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Service;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class ExpensesDependencies
    {
        public static void AddExpensesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExpensesRepository,ExpensesRepository>();

            services.AddTransient<IExpensesService,ExpensesService>();
        }
    }
}
