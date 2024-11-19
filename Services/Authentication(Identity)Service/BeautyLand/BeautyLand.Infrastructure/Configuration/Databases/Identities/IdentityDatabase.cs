using BeautyLand.Domain.Role;
using BeautyLand.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.Configuration.Databases.IdentityServer
{
    public static class IdentityDatabase
    {
        public static IServiceCollection AddIdentityServerDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Connections:IdentityServerConnectionString"];
            services.AddDbContext<Persistence.Databases.IdentityServer.IdentityDatabase>(configureOptions =>
            {
                configureOptions.UseSqlServer(connectionString);
            }); 
          
            return services;
        }
    }
}
