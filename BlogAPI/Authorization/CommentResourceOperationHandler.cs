using BlogAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogAPI.Authorization
{
    public class CommentResourceOperationHandler : AuthorizationHandler<ResourceOperationRequirement, Comment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceOperationRequirement requirement, Comment comment)
        {
            if (requirement.resourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            if(requirement.resourceOperation == ResourceOperation.Delete)
            {
                var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

                if (comment.CreatedById == userId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
