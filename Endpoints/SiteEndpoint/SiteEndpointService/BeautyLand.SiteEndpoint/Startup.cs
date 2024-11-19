using BeautyLand.Application.Services.Site.Baskets.Basket;
using BeautyLand.Application.Services.Site.Catalogs.Item;
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.Application.Services.Site.Discounts.GRPC;
using BeautyLand.Application.Services.Site.Orders;
using BeautyLand.Application.Services.Site.Payments;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostEnvironment _environment;

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcService = services.AddControllersWithViews();
            if (_environment.IsDevelopment())
            {
                mvcService.AddRazorRuntimeCompilation();
            }
            //Services 
            services.AddHttpContextAccessor();

            services.AddMetrics();

            services.AddMetricsTrackingMiddleware();

            services.Configure<KestrelServerOptions>(p =>
            {
                p.AllowSynchronousIO = true;
            });

            //This service is related to IdentityModel.AspNetCore configuration for refreshtoken 
            services.AddAccessTokenManagement();    

          //services.AddScoped<IDiscountService, Application.Services.Site.Discounts.GRPC.DiscountService>();

            services.AddScoped<IItemService, ItemService>(p =>
            {
                return new ItemService(new RestClient(Configuration["MicroserviceAddresses:ApiGatewayAddresses:Uri"]));
            });

            services.AddScoped<IBasketService, BasketService>(p =>
            {
                return new BasketService(new RestClient(Configuration["MicroserviceAddresses:ApiGatewayAddresses:Uri"]));

            });
            //IdentityModel.AspNetCore configuration and supports HttpClient not Restsharp
            //We do not need adding getAccessToken to Authorization the header of rest with adding this method AddUserAccessTokenHandler() in IOC
            services.AddHttpClient<IOrderService, OrderService>(p =>
            {
                p.BaseAddress = new Uri(Configuration["MicroserviceAddresses:ApiGatewayAddresses:Uri"]);
            }).AddUserAccessTokenHandler();

            services.AddScoped<IPaymentsService, PaymentService>(p =>
            {
                return new PaymentService(new RestClient(Configuration["MicroserviceAddresses:ApiGatewayAddresses:Uri"]));
            });

            services.AddScoped<IDiscountService, Application.Services.Site.Discounts.REST.DiscountService>(p =>
            {
                return new Application.Services.Site.Discounts.REST.DiscountService(new RestClient(Configuration["MicroserviceAddresses:ApiGatewayAddresses:Uri"]));
            });

            services.AddAuthentication(options => 
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie
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
                   options.ClientId = "5363D3C5-73CA-4E0D-98A9-75DD96703641";
                   options.ClientSecret = "91703832";
                   options.ResponseType = "code";
                   options.GetClaimsFromUserInfoEndpoint = true;
                   //Accoring to change this value of property, we should use IHttpContextAccessor in view service for receiving token from cookies through IdentityServer 
                   options.SaveTokens = true;
                   //It sets these identit resources as default value
                   //options.Scope.Add("profile");
                   //options.Scope.Add("openid");
                   options.Scope.Add("OrderSevice.Controller");
                   options.Scope.Add("ApiGateWayService.Service");
                   options.Scope.Add("offline_access");
               }
             );
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

            app.UseMetricsAllMiddleware();  

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
