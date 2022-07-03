using BlogAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogAPI.Authorization
{
    public class ResourceOperationHandler : AuthorizationHandler<ResourceOperationRequirement, Blog>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceOperationRequirement requirement, Blog blog)
        {
            if(requirement.resourceOperation== ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if(blog.CreatedById == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
