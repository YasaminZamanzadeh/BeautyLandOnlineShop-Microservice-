using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Payments;
using BeautyLand.Infrastructure.Configurations.Databases.Catalogs;
using BeautyLand.Infrastructure.Messages.MessagesBus.Consumer;
using BeautyLand.Infrastructure.Messages.MessagesBus.Publisher;
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
            //PaymentDatabase
            services.AddTransient<IPaymentDatabaseService, Persistence.Databases.Payments.PaymentDatabase>();
            services.AddPaymentDatabaseConfig(Configuration);

            //RabbitMQOrderRequestPaymentConfiguration
            services.Configure<Infrastructure.Messages.MessagesBus.Dtos.Consumer.RabbitMQOrderConfigurationDto>(Configuration.GetSection("RabbitMQOrderRequestPaymentConfiguration"));
            services.Configure< Infrastructure.Messages.MessagesBus.Dtos.Publisher.RabbitMQOrderConfigurationDto> (Configuration.GetSection("RabbitMQPaymentRequestOrderConfiguration"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeautyLand.SiteEndPoint", Version = "v1" });
            });

            //Services
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddHostedService<RabbitMQOrderConsumer>();
            services.AddTransient<IMessageBus, RabbitMQOrderPublisher>();
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
