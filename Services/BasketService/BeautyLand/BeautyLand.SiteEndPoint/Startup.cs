using BeautyLand.Application.Messages.MessagesBus.Consumer;
using BeautyLand.Application.Messages.MessagesBus.Dtos.Consumer;
using BeautyLand.Application.Services.Databases.Baskets;
using BeautyLand.Application.Services.Site.BasketItems.BasketItems;
using BeautyLand.Application.Services.Site.Baskets.Bskets;
using BeautyLand.Application.Services.Site.Catalogs.Items;
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.Infrastructure.Configurations.Databases.Catalogs;
using BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher;
using BeautyLand.Subscription.Messages.MessagesBus.Publisher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndPoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //BasketDatabase
            services.AddTransient<IBasketDatabaseService, Persistence.Databases.Basket.BasketDatabase>();
            services.AddItemDatabaseConfig(Configuration);

            //Service
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IBasketItemService, BasketItemService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddHostedService<RabbitMQItemConsumer>();

            //MessageBus
            services.AddScoped<IMessagePublisher, RabbitMQBasketPublisher>();

            //RabbitMQConfiguration
            services.Configure<RabbitMQBasketConfigurationDto>(Configuration.GetSection("RabbitMQBasketCheckoutConfiguration"));
            services.Configure<RabbitMQItemConfigurationDto>(Configuration.GetSection("RabbitMQItemChangesConfiguration"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeautyLand.SiteEndPoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeautyLand.SiteEndPoint v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                endpoints.MapControllers();
            });

        }
    }
}
