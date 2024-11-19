using BeautyLand.Domain.Role;
using BeautyLand.Domain.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.SeedData.IdentityUser
{
    public static class IdentityUserRoleData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<Persistence.Databases.IdentityServer.IdentityDatabase>();
                context.Database.EnsureCreatedAsync();
                if (context.Users.Count() == 0)
                {
                    Role adminRole = new Role
                    {
                        Name = "Admin"
                    };

                    Role userRole = new Role
                    {
                        Name = "User"
                    };

                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                    roleManager.CreateAsync(adminRole).Wait();
                    roleManager.CreateAsync(userRole).Wait();

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                    foreach (var item in UserList())
                    {
                        var result = userManager.CreateAsync(item, "P@ssword7887").Result;
                        if (item.UserName == "YasaminZamanzadeh")
                        {
                            var user = userManager.AddToRoleAsync(item, "User").Result;
                        }
                        else
                        {
                            var admin = userManager.AddToRoleAsync(item, "Admin").Result;
                        }
                    }
                }
            }
        }


        private static List<User> UserList()
        {
            return new List<User>
        {
            new User()
            {
                UserName = "YasaminZamanzadeh",
                Email = "Zamanzadehyasamin@yahoo.com",
                EmailConfirmed = true,
            },
            new User()
            {
                UserName = "AliPashaei",
                Email = "PashaeiAli@yahoo.com",
                EmailConfirmed = true,
                  }
             };
        }
    }
}