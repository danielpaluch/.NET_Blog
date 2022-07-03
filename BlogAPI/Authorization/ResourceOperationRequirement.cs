using Microsoft.AspNetCore.Authorization;

namespace BlogAPI.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation resourceOperation { get; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            this.resourceOperation = resourceOperation;
        }
    }
}
