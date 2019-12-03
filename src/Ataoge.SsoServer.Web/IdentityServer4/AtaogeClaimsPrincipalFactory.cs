using System.Security.Claims;
using System.Threading.Tasks;
using Ataoge.SsoServer.Web.Data;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Ataoge.SsoServer.Web
{
    public class AtaogeClaimsPrincipalFactory<TUser, TRole>: UserClaimsPrincipalFactory<TUser, TRole> 
        where TUser : ApplicationUser
        where TRole : ApplicationRole
    {
        public AtaogeClaimsPrincipalFactory(
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager, 
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
    
        }
    
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var id = await base.GenerateClaimsAsync(user);

            var displayName = user.GetDisplayName();
            id.AddClaim(new Claim("displayname", displayName));


            if (UserManager.SupportsUserEmail)
            {
                var email = await UserManager.GetEmailAsync(user);
                if (!string.IsNullOrWhiteSpace(email))
                {
                    var md5 = System.Security.Cryptography.MD5.Create();
                    var bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email.ToLower()));
                    
                    
                    string  md5email = null;
                    for   (int   i=0;   i<bytes.Length;   i++)   
                    {
                        md5email   +=   bytes[i].ToString("x");
                    }

                    // d = 404, mm, identicon, monsterid, wavatar, retro, blank
                    var picUrl = string.Format("https://www.gravatar.com/avatar/{0}?d=identicon&s=128", md5email);
                    id.AddClaim(new Claim(JwtClaimTypes.Picture, picUrl));
                }
            }
           
            //id.AddClaim(new Claim(MyClaimTypes.IsAdmin, user.IsAdministrator.ToString().ToLower()));
            return id;
        }

        
    
    }
}