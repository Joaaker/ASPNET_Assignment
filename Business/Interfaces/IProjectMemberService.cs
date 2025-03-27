using Domain.Dtos;

namespace Business.Interfaces;

public interface IProjectMemberService
{
    Task<IResponseResult> UpdateProjectServicesAsync(int projectId, List<string> currentMemberIds, List<string> newMemberIds);
    Task<IResponseResult> DeleteProjectServiceAsync(int projectId, string memberId);
}