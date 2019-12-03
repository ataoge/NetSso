using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Diagnostics
{
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsModel : PageModel
    {
        public DiagnosticsViewModel Input {get; set;}
        
        public async Task OnGetAsync()
        {
            Input = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
        }
    }
}