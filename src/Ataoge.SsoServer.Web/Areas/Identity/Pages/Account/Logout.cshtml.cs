using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ataoge.SsoServer.Web.Data;
using Ataoge.SsoServer.Web.Services;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        private readonly IOnlineUserService _onlineUserService;

        [BindProperty]
        public LogoutViewModel Input {get; set;}

        [TempData]
        public string LoggedOutViewModel { get; set; }

        public LogoutModel(IIdentityServerInteractionService interaction,
                IEventService events,
                SignInManager<ApplicationUser> signInManager, 
                IOnlineUserService onlineUserService,
                ILogger<LogoutModel> logger)
        {
            _interaction = interaction;
            _events = events;

            _signInManager = signInManager;
            _onlineUserService = onlineUserService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string logoutId)
        {
           
            // build a model so the logout page knows what to display
            Input = await BuildLogoutViewModelAsync(logoutId);
            if (Input.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await OnPostAsync(Input);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(LogoutInputModel model, string returnUrl = null)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                //注销用户
                var userAgent = Request.Headers["User-Agent"];
                var userName = User.Identity.Name;
                if (!string.IsNullOrEmpty(userName))
                    _onlineUserService.Remove(userName.ToLower(), userAgent);


                 _logger.LogInformation("User logged out.");

                // delete local authentication cookie
                //await HttpContext.SignOutAsync();
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            
            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Page("./Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                //return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
                return new SignOutResult(vm.ExternalAuthenticationScheme, new AuthenticationProperties { RedirectUri = url});
            }

            if (string.IsNullOrEmpty(model.LogoutId))
            {
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return LocalRedirect(Url.Content("~/")); //RedirectToPage();
                }
            }

            LoggedOutViewModel = JsonSerializer.Serialize(vm);
            return RedirectToPage("./LoggedOut");
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}
