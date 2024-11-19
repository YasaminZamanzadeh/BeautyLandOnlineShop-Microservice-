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
    public static class PaymentDatabase
    {
        public static IServiceCollection AddPaymentDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Connections:PaymentsConnectionString"];
            services.AddDbContext<Persistence.Databases.Payments.PaymentDatabase>(configureOptions =>
            {
                configureOptions.UseSqlServer(connectionString);
            },
           ServiceLifetime.Singleton
            );
            return services;
        }
    }
}
