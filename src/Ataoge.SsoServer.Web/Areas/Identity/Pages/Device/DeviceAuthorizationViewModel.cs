using Ataoge.SsoServer.Web.Areas.Identity.Pages.Consent;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}