namespace  Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}