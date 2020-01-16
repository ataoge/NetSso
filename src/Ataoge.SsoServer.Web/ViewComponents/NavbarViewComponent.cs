using System.Collections.Generic;
using System.Threading.Tasks;
using Ataoge.SsoServer.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ataoge.SsoServer.Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IAuthorizationService _authorizationService;

        public NavbarViewComponent(IAuthorizationService authorizationService)
        {
            this._authorizationService = authorizationService;
        }

        public IViewComponentResult Invoke()
        {
            var menus = new List<MenuItem>();
            //menus.Add(new MenuItem() {Name = "首页", Url = "Index"});
            //menus.Add(new MenuItem() {Name = "隐私", Url = "Privacy"});

            if (User.Identity.IsAuthenticated)
            {
                menus.Add(new MenuItem() {Name = "关于", Url =  Url.Action("Index", "Home")});
            }

            if (User.IsInRole("admin"))
            {
                menus.Add(new MenuItem() {Name = "在线用户", Url = Url.Action("Online", "User")});
            }
            
            
            return View("Default", menus);
        }
    }
}