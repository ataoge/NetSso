namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{
    public class ConfirmDeviceLoginViewModel
    {
        public string ConnectedId {get; set;}

        public string ClientId {get;set;}

        public string ClientName {get; set;}
        public string AccessToken {get; set;}
        public string OpenId {get;set;}
    
    }
}