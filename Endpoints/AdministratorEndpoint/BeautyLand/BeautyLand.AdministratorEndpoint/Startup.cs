using BeautyLand.Application.Administrator.ViewServices.Catalogs.Items;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyLand.AdministratorEndpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddAuthorization();
            services.AddControllersWithViews();
            services.AddScoped<IItemService, ItemService>(p =>
            {
                return new ItemService(new RestClient(Configuration["MicroserviceAddresses:ApiGateways:Uri"]), new HttpContextAccessor());
            });
            services.AddHttpContextAccessor();

            services.AddAuthentication
               (
                options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
               .AddCookie
               (
                authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme
                )
               .AddOpenIdConnect
               (
                authenticationScheme: OpenIdConnectDefaults.AuthenticationScheme,
                options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Authority = "https://localhost:44393";
                    options.ClientId = "C7451CD0-23DD-4BF9-A762-78908179333A";
                    options.ResponseType = "code";
                    options.SaveTokens = true;
                    options.ClientSecret = "23830719";
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Add
                    (
                        item: "openid"
                    );
                    options.Scope.Add
                    (
                       item: "profile"
                       );
                    options.Scope.Add
                    (
                        item: "CatalogService.Service"
                        );
                    options.Scope.Add
                    (
                        item: "AdministratorApiGateWayService.Service"
                        );
                    options.Scope.Add
                    (
                        item:"roles"
                        );
                    options.ClaimActions.MapUniqueJsonKey
                    (
                        claimType: "role",
                        jsonKey: "role"
                        );
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role",
                    };
                });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
