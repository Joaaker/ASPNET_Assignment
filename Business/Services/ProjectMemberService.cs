using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectMemberService(IProjectMemberRepository projectMemberRepository) : IProjectMemberService
{
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;

    public async Task<IResponseResult> DeleteProjectMembersAsync(int projectId, string memberId)
    {
        //Transactions are handled in UpdateProjectMembersAsync()
        try
        {
            var projectMembersJunctionEntity = await _projectMemberRepository.GetAsync(x => x.ProjectId == projectId && x.UserId == memberId);
            if (projectMembersJunctionEntity == null)
                return ResponseResult.NotFound("ProjectMember not found");

            await _projectMemberRepository.DeleteAsync(x => x.ProjectId == projectId && x.UserId == memberId);
            var saveResult = await _projectMemberRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error occurred while saving the deletion.");

            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting ProjectMember :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> UpdateProjectMembersAsync(int projectId, List<string> currentMemberIds, List<string> newMemberIds)
    {
        try
        {
            if (currentMemberIds.SequenceEqual(newMemberIds))
                return ResponseResult.Ok();

            var toRemove = currentMemberIds.Except(newMemberIds).ToList();
            var toAdd = newMemberIds.Except(currentMemberIds).ToList();

            await _projectMemberRepository.BeginTransactionAsync();

            foreach (string memberId in toRemove)
            {
                var deleteResponse = await DeleteProjectMembersAsync(projectId, memberId);
                if (deleteResponse.Success == false)
                    throw new Exception($"Error deleting ProjectMember :: {deleteResponse.ErrorMessage}");
            }
            foreach (var memberId in toAdd)
            {
                var projectMembersJunctionEntity = ProjectMembersFactory.CreateEntity(projectId, memberId);
                await _projectMemberRepository.AddAsync(projectMembersJunctionEntity);
                var psAddSaveResult = await _projectMemberRepository.SaveAsync();
                if (psAddSaveResult == false)
                    throw new Exception($"Error saving updated ProjectMember");
            }
            await _projectMemberRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectMemberRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating ProjectMembers :: {ex.Message}");
        }
    }
}