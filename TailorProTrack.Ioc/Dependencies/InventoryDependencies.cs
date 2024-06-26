﻿using Microsoft.Extensions.DependencyInjection;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.BuyInventoryContracts;
using TailorProTrack.Application.Service;
using TailorProTrack.Application.Service.BuyInventoryService;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Ioc.Dependencies
{
    public static class InventoryDependencies
    {
        public static void AddInventoryDependencies(this IServiceCollection service)
        {
            service.AddScoped<IInventoryRepository, InventoryRepository>();

            service.AddScoped<IInventoryColorRepository, InventoryColorRepository>();


            service.AddTransient<IInventoryColorService, InventoryColorService>();

            service.AddTransient<IInventoryService, InventoryService>();
            service.AddTransient<IBuyInventoryService, BuyInventoryService>();


        }
    }
}
