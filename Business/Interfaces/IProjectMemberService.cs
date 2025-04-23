using Domain.Dtos;

namespace Business.Interfaces;

public interface IProjectMemberService
{
    Task<IResponseResult> UpdateProjectMembersAsync(int projectId, List<string> currentMemberIds, List<string> newMemberIds);
    Task<IResponseResult> DeleteProjectMembersAsync(int projectId, string memberId);
}