using Data.Entities;

namespace Business.Factories;

public class ProjectMembersFactory
{
    public static ProjectMemberJunctionEntity CreateEntity(int projectId, string userId) => new()
    {
        ProjectId = projectId,
        UserId = userId,
    };
}