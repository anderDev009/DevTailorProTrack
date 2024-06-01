
using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Contracts.ProductColor;
using TailorProTrack.Application.Contracts.ProductSize;
using TailorProTrack.Application.Service;
using TailorProTrack.Application.Service.Filter;
using TailorProTrack.Application.Service.ProductSizeService;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class ProductDependencies
    {
        public static void AddProductDependencies(this IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IProductColorRepository, ProductColorRepository>();
            service.AddScoped<IProductSizeRepository, ProductSizeRepository>();

            //servicios
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IProductFilterService, ProductFilterService>();
            service.AddScoped<IProductColorService,ProductColorService>();
            service.AddScoped<IProductSizeService,ProductSizeService>();

        }
    }
}
