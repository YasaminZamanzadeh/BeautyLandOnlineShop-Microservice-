using BeautyLand.Application.Services.Databases.Discounts;
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.Infrastructure.Configurations.Databases.Catalogs;
using BeautyLand.SiteEndPoint.GRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace BeautyLand.SiteEndPoint
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
            //BasketDatabase
            services.AddTransient<IDiscountDatabaseService, Persistence.Databases.Discounts.DiscountDatabase>();
            services.AddItemDatabaseConfig(Configuration);

            //Service
            services.AddTransient<IDiscountServices, DiscountService>();
            services.AddGrpc();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeautyLand.SiteEndPoint", Version = "v1" });
            });
        }

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
                endpoints.MapGrpcService<DiscountGRPC>();
                endpoints.MapControllers();
            });

        }
    }
}
