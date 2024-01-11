

using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class UserDependencies
    {
        public static void AddUserDependencies(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            //service.AddTransient<IUserser, UserRepository>();
        }
    }
}
