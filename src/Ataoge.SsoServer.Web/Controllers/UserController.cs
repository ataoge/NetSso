using Ataoge.SsoServer.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ataoge.SsoServer.Web.Controllers
{
    [Authorize]
    public class UserController  : Controller
    {
        private readonly IOnlineUserService _onlineUserService;

        private readonly IAuthorizationService _authorizationService;

        public UserController(IOnlineUserService onlineUserService,
            IAuthorizationService authorizationService)
        {
            this._onlineUserService = onlineUserService;
            this._authorizationService = authorizationService;
        }

        public IActionResult Online()
        {
            var users = _onlineUserService.GetAll();
            return View(users);
        }
    }
}