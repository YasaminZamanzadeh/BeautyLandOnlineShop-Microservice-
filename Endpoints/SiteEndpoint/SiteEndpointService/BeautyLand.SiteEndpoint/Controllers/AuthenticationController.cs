using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndpoint.Controllers
{
    public class AuthenticationController : Controller
    {
        public async Task Signout()
        {
            //It will signout of application 
             await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //It will signout of IdentityServer
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
