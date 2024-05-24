using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MinimalApiTutorial.Auth
{
    public class SelfOnlyPolicyHandler : AuthorizationHandler<SelfOnlyRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SelfOnlyRequirement requirement)
        {
            if (requirement.Id == requirement.TargetId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
