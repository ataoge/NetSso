using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoggedOutModel : PageModel
    {
        [TempData]
        public string LoggedOutViewModel { get; set; }

        public LoggedOutViewModel Input {get; set;}


        public void OnGet()
        {
            if (!string.IsNullOrEmpty(LoggedOutViewModel))
            {
                Input =  JsonSerializer.Deserialize<LoggedOutViewModel>(LoggedOutViewModel);
            }
        }
    }
}