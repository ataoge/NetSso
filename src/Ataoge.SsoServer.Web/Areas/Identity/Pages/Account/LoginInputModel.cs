using System.ComponentModel.DataAnnotations;

namespace Ataoge.SsoServer.Web.Areas.Identity.Pages.Account
{

    public class LoginInputModel
        {
            [Required]
            [EmailOrPhone]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "记住我?")]
            public bool RememberMe { get; set; }

            [Display(Name = "动态验证")]
            public bool DynamicVerify {get; set;}

            public string WelcomeDescription {get; set;}

            public string ReturnUrl { get; set; }
        }
}