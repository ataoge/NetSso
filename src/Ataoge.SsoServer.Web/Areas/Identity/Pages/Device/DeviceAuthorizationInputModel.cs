using Ataoge.SsoServer.Web.Areas.Identity.Pages.Consent;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}
