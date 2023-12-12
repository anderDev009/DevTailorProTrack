using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class TypeProductDependencies
    {
        public static void AddTypeProductDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITypeProdRepository, TypeProdRepository>();
        }
    }
}
