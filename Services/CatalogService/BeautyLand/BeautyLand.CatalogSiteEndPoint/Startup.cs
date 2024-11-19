using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Catalogs.Items;
using BeautyLand.Application.Services.Site.Catalogs.Types;
using BeautyLand.Infrastructure.Configurations.Databases.Catalogs;
using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;
using BeautyLand.Infrastructure.Messages.MessagesBus.Publisher;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Define aliases for the namespaces
using MetricsExtensions = Microsoft.Extensions.DependencyInjection.MetricsServiceExtensions;



namespace BeautyLand.CatalogSiteEndPoint
{ 
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()  
                .ReadFrom.Configuration(configuration)
                .CreateLogger();    
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ItemDatabase
            services.AddTransient<ICatalogDatabaseService, Persistence.Databases.Payments.CatalogDatabase>();
            services.AddCatalogDatabaseConfig(Configuration);

            //Service



            services.AddHealthChecks()
                .AddSqlServer(
                connectionString: "Data Source = .; Initial Catalog = CatalogBeautyLand; Integrated Security = True; Encrypt=false;",
                tags: new string[]
                {
                    "CatalogDataase",
                    "SqlServer"
                })
                .AddRabbitMQ(
                rabbitConnectionString: "amqp://localhost:5672",
                tags: new string[]
                {
                    "RabbitMQ"
                });
            //.AddCheck<CatalogDatabaseHealthCheck>("SQLDatabaseCheck");

            services.AddHealthChecksUI(p=> p.AddHealthCheckEndpoint(
                name: "CatalogHealthCheckUI",
                uri: "/health" 
                )).AddInMemoryStorage();



            // Use the alias to call the method
            MetricsExtensions.AddMetrics(services);  
            services.AddMetricsTrackingMiddleware();
            services.Configure<KestrelServerOptions>(p =>
            {
                p.AllowSynchronousIO = true;
            });


            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<ITypeService, TypeService>();
            services.AddTransient<IMessageBus, RabbitMQAllPublisher>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeautyLand.SiteEndPoint", Version = "v1" });
            });

            //RebbitMQConfiguration
            services.Configure<RabbitMQAllConfigurationDto>(Configuration.GetSection("RabbitMQItemChangesConfiguration"));

            //IdentityServerConfiguration
            services.AddAuthentication
                (

                options =>
                {
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                )
                .AddJwtBearer
                (
                options =>
                {
                    options.Authority = "https://localhost:44393";
                    options.Audience = "CatalogService";
                });


            services.AddAuthorization
              (
              options =>
              {
                  options.AddPolicy
                  (
                      name: "CatalogControllerPolicy",
                     policyOptions =>
                     {
                         policyOptions.RequireClaim
                          (
                             claimType: "scope",
                             allowedValues: new[]
                                {
                                    "CatalogService.Service",
                                    "AdministratorApiGateWayService.Service"
                                  }
                             );
                     });
              });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Persistence.Databases.Payments.CatalogDatabase catalogContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeautyLand.SiteEndPoint v1"));
            }

            //catalogContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseMetricsAllMiddleware();


            app.UseHealthChecks(
                //The UI is this laibrary Xabaril/AspNetCore.Diagnostics.HealthChecks
                path: "/health",
                options: new HealthCheckOptions
                { 
                 Predicate =
                    p=> true,
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                });

            app.UseHealthChecksUI(delegate (Options options)
            {
                options.UIPath = "/healthUI";
                options.ApiPath = "/healthAPI";
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );
                endpoints.MapControllers();
                //UI internal AspNetCore serivece of health check
                //endpoints.MapHealthChecks("/health");
            });

        }
    }
}
