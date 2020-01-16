using Microsoft.AspNetCore.Mvc;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{
    public class TokenInputModel
    {
        [HiddenInput]
        public string TempToken {get; set;}
    }
}