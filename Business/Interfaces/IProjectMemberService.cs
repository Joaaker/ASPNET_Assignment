using Domain.Dtos;

namespace Business.Interfaces;

public interface IProjectMemberService
{
    Task<bool> UpdateProjectServicesAsync(int projectId, List<string> currentMemberIds, List<string> newMemberIds);
    Task<bool> DeleteProjectServiceAsync(int projectId, string memberId);
}