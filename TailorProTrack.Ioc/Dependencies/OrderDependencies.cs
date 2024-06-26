﻿
using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Report;
using TailorProTrack.Application.Service;
using TailorProTrack.Application.Service.Reports;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class OrderDependencies
    {   
        public static void AddOrderDependencies(this IServiceCollection service)
        {
            //repositorios
            service.AddScoped<IOrderRepository,OrderRepository>();
            service.AddScoped<IOrderProductRepository,OrderProductRepository>();

            //servicios
            service.AddTransient<IOrderService,OrderService>();
            service.AddTransient<IOrderProductService,OrderProductService>();
            service.AddTransient<IReportOrderInventoryService, ReportOrderInventoryService>();

        }
    }
}
