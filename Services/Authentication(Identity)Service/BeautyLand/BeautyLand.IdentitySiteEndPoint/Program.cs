using BeautyLand.Domain.Role;
using BeautyLand.Domain.Users;
using BeautyLand.Infrastructure.Configuration.Databases.IdentityServer;
using BeautyLand.Infrastructure.SeedData.IdentityUser;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

//Service
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//IdentityConfiguration
builder.Services.AddIdentity<User, Role>()
              .AddEntityFrameworkStores<BeautyLand.Persistence.Databases.IdentityServer.IdentityDatabase>()
              .AddDefaultTokenProviders();

builder.Services.AddIdentityServerDatabaseConfig(configuration);

//IdentityServerConfiguration
builder.Services
    .AddIdentityServer()
   .AddDeveloperSigningCredential()
    //.AddTestUsers
    //(
    //new List<TestUser>
    //{
    //    new TestUser
    //    {
    //        Username = "userName",
    //        Password = "P@ssw0rd",
    //        IsActive = true,
    //        SubjectId = "1"
    //    }
    //})
   .AddAspNetIdentity<User>()
   .AddInMemoryClients
    (
    new List<Client>
    {
        //new Client
        //{
        //    ClientName = "SiteEndPoint",
        //    ClientSecrets =
        //    {
        //        new Secret
        //        (
        //            "23830719".Sha256()
        //       )},
        //    ClientId = "B14EC808-C1CD-40E4-A9F0-5D818ABCA98A",
        //    AllowedScopes = new[]
        //    {
        //     "OrderSevice.Controller",
        //    },
        //    //The enum of GrantTypes with ClientCredentials value calls an API and sends acess token
        //    AllowedGrantTypes = GrantTypes.ClientCredentials
        // },
        new Client
        {
            ClientName = "Users",
            ClientSecrets =
            {
                new Secret
                (
                    "91703832".Sha256()
                 )},
            ClientId = "5363D3C5-73CA-4E0D-98A9-75DD96703641",
            //The enum of GrantTypes with Code value guides to Identity server fo signin and the desired data callsback to the desired url 
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = { "https://localhost:44305/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:44305/signout-callback-oidc" },
            AllowedScopes =
            {
                  "openid",
                  "profile",
                  "OrderSevice.Controller",
                  "ApiGateWayService.Service"
            },
            //When token will be sent,it implements refreshtoken and is accessable through offline mode
            AllowOfflineAccess = true,
            //Each token will have been activing 60 seconds, after it will take refresh token
            AccessTokenLifetime = 60,
            RefreshTokenUsage = TokenUsage.ReUse,
            RefreshTokenExpiration = TokenExpiration.Sliding
        },
         new Client
         {
             ClientName = "Admins",
             ClientSecrets =
            {
                new Secret
                (
                    "23830719".Sha256()
                 )},
             ClientId = "C7451CD0-23DD-4BF9-A762-78908179333A",
             AllowedGrantTypes = GrantTypes.Code,
             RedirectUris = { "https://localhost:44305/signin-oidc" },
             PostLogoutRedirectUris = { "https://localhost:44305/signout-callback-oidc" },
             AllowedScopes =
            {
                 "openid",
                 "profile",
                 "CatalogService.Service",
                 "AdministratorApiGateWayService.Service",
                 "roles"
            }
      }
   })
    .AddInMemoryIdentityResources
    (
    new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
      new IdentityResource
      (
          name: "roles",
          userClaims: new List<string>
          {
              "role"
          }
          )
        })
    .AddInMemoryApiScopes
    (
     new List<ApiScope>
     {
         new ApiScope
     (
          name: "OrderSevice.Controller",
         displayName: "Order service scope"
       ),

       new ApiScope
     (
          name: "ApiGateWayService.Service",
         displayName: "Api gateway service scope"
     ),
       new ApiScope
       (
           name: "CatalogService.Service",
           displayName: "Catalog service scope"
      ),
             new ApiScope
     (
          name: "AdministratorApiGateWayService.Service",
         displayName: "Administrator api gateway scope",
         userClaims: new List<string>
         {
                 "role"
          }
     )
     })
    .AddInMemoryApiResources
    (
    new List<ApiResource>
    {
    new ApiResource
        (
         name: "OrderService",
         displayName: "Order service api"
        )
    {
        Scopes =new []
        {
            "OrderSevice.Controller"
        }
    },

    new ApiResource
    (
        name: "ApiGateWayService",
        displayName: "Api gateway service"
        )
        {
          Scopes =new []
        {
            "ApiGateWayService.Service"
        }
    },
    new ApiResource
    (
        name: "CatalogService",
        displayName: "Catalog service api"
        )
    {
        Scopes = new []
        {
            "CatalogService.Service"
        }
    },
    new ApiResource
    (
        name: "AdministratorApiGatewayService",
        displayName: "Administrator api gateway service"
        )
    {
        Scopes = new []
        {
              "AdministratorApiGateWayService.Service"
        }
    }
    });

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    //IdentityUserRoleData.Seed(app);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
