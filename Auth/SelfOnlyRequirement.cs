using Microsoft.AspNetCore.Authorization;

namespace MinimalApiTutorial.Auth
{
    public class SelfOnlyRequirement:IAuthorizationRequirement
    {
        public SelfOnlyRequirement(int id, int targetId)
        {
            Id = id;
            TargetId = targetId;
        }

        public int Id { get; init; }

        public int TargetId { get; init; }

    }
}
