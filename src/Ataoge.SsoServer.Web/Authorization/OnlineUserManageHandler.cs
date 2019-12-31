using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Ataoge.SsoServer.Web.Authorization
{
    public class OnlineUserManageHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, object resource)
        {
            if (context.User.IsInRole("admin") &&
                requirement.Name == Operations.Edit.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}