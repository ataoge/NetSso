using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Ataoge.SsoServer.OpenIdClient.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login()
        {
            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated) 
            {
                var properties = new AuthenticationProperties { RedirectUri = Request.PathBase +   "/" };
                await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
            }
                
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                
            }
        }
    }
}