using System.Linq;
using System.Threading.Tasks;
using Ataoge.SsoServer.WebSockets;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ComfirmDeviceLoginModel : PageModel
    {
        private readonly  ITokenValidator _tokenValidator;
        private readonly LoginManager _loginManager;
        private readonly LoginMessageHandler _handle;

        public ComfirmDeviceLoginModel(ITokenValidator tokenValidator,
                LoginManager loginManager,
                LoginMessageHandler handle)
        {
            this._tokenValidator = tokenValidator;
            this._loginManager = loginManager;
            this._handle = handle;
        }

        [BindProperty]
        public ConfirmDeviceLoginViewModel Input {get; set;}

        public async Task<IActionResult> OnGetAsync(string secret)
        {
            var accessToken = GetTokenFromHeOader(this.HttpContext);
            if (!string.IsNullOrEmpty(accessToken))
            {
                var tokenResult = await _tokenValidator.ValidateAccessTokenAsync(accessToken);
                if (!tokenResult.IsError) {
                    var subClaim = tokenResult.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.Subject);
                    var openId = subClaim.Value;
                    
                    Input = new ConfirmDeviceLoginViewModel() { ConnectedId = secret, ClientName = tokenResult.Client.ClientName, ClientId = tokenResult.Client.ClientId, AccessToken = accessToken, OpenId= openId };
                    return Page();
                }
            }
                    
            return this.StatusCode(401);
        }

 

        private string GetTokenFromHeOader(HttpContext httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                var header = authorizationHeader.Trim();
                if (header.StartsWith(OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer))
                {
                    var value = header.Substring(OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer.Length).Trim();
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
            }
            return null;
        }

         public async Task<IActionResult> OnPostAsync(string action)
         {
            if (ModelState.IsValid)
            {
                if (action == "OK")
                {
                    var tokenResult = await _tokenValidator.ValidateAccessTokenAsync(Input.AccessToken);
                    if (!tokenResult.IsError) {
                         var subClaim = tokenResult.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.Subject);
                         if (Input.OpenId == subClaim.Value)
                         {
                            var token = _loginManager.GetTempToken(Input.OpenId);
                            await _handle.SendLoginMessageAsync(Input.ConnectedId, token);
                        }
                    }
                }
                return RedirectToPage("./Close");
            }

            return this.StatusCode(500);
         }

    }

}