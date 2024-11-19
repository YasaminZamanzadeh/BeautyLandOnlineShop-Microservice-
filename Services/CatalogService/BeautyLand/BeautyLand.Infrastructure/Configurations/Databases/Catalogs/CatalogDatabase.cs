using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.Configurations.Databases.Catalogs
{
    public static class CatalogDatabase
    {
        public static IServiceCollection AddCatalogDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Connections:CatalogsConnectionString"];
            services.AddDbContext<Persistence.Databases.Payments.CatalogDatabase>(configureOptions =>
            {
                configureOptions.UseSqlServer(connectionString);
            },
           ServiceLifetime.Singleton
            );
            return services;
        }
    }
}
