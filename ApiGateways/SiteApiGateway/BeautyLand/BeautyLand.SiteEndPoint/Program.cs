using BeautyLand.Application.Services.Site.Discounts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Ocelot.Cache.CacheManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Values;
using Microsoft.Extensions.Options;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeautyLand.SiteEndPoint", Version = "v1" });
});
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment webHostEnvironment = builder.Environment;

// Services

builder.Services.AddControllers();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services
    .AddOcelot(configuration)
    .AddPolly()
    .AddCacheManager(x => x.WithDictionaryHandle()); ;

//Ocelot configuration
builder.Configuration
    .SetBasePath(webHostEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddOcelot(webHostEnvironment)
    .AddEnvironmentVariables();


//IdentityConfiguration 
const string AuthenticationProviderKey = "ApiGateWayAuthenticationScheme";

builder.Services.AddAuthentication
     (
    options =>
      {
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       }).
       AddJwtBearer
        (
        authenticationScheme: AuthenticationProviderKey,
        options =>
        {
            options.Authority = "https://localhost:44393";
            options.Audience = "ApiGateWayService";
        });

var app = builder.Build();

if (app.Environment.IsDevelopment())
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
    endpoints.MapControllers();
   
});


app.UseOcelot().Wait();

app.Run();