using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class ProjectMemberService(IProjectMemberRepository projectMemberRepository) : IProjectMemberService
{
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;

    public Task<bool> DeleteProjectServiceAsync(int projectId, string memberId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProjectServicesAsync(int projectId, List<string> currentMemberIds, List<string> newMemberIds)
    {
        throw new NotImplementedException();
    }
}
