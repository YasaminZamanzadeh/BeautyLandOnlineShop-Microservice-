﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.Configurations.Databases.Catalogs
{
    public static class BasketDatabase
    {
        public static IServiceCollection AddItemDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Connections:BasketsConnectionString"];
            services.AddDbContext<Persistence.Databases.Basket.BasketDatabase>(configureOptions =>
            {
                configureOptions.UseSqlServer(connectionString);
            },
            ServiceLifetime.Singleton);
            return services;
        }
    }
}