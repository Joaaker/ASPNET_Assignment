using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectMemberService(IProjectMemberRepository projectMemberRepository) : IProjectMemberService
{
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;

    public async Task<IResponseResult> DeleteProjectServiceAsync(int projectId, string memberId)
    {
        //Transactions are handled in ProjectService.UpdateProjectAsync()
        try
        {
            var projectServiceJunctionEntity = await _projectMemberRepository.GetAsync(
                x => x.ProjectId == projectId && x.UserId == memberId);
            if (projectServiceJunctionEntity == null)
                return ResponseResult.NotFound("ProjectService not found");

            await _projectMemberRepository.DeleteAsync(
                x => x.ProjectId == projectId && x.UserId == memberId);
            var saveResult = await _projectMemberRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error occurred while saving the deletion.");

            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting ProjectService :: {ex.Message}");
        }

    }

    public async Task<IResponseResult> UpdateProjectServicesAsync(int projectId, List<string> currentMemberIds, List<string> newMemberIds)
    {
        if (currentMemberIds == null || newMemberIds == null)
            return ResponseResult.BadRequest("Invalid update ProjectService input");
        //Transactions are handled in ProjectService.UpdateProjectAsync()
        try
        {
            if (currentMemberIds.SequenceEqual(newMemberIds))
                return ResponseResult.Ok();

            var toRemove = currentMemberIds.Except(newMemberIds).ToList();
            var toAdd = newMemberIds.Except(currentMemberIds).ToList();

            foreach (string memberId in toRemove)
            {
                var deleteResponse = DeleteProjectServiceAsync(projectId, memberId);
                if (deleteResponse.Result.Success == false)
                    throw new Exception("Error deleting ProjectService");
            }

            foreach (var serviceId in toAdd)
            {
                var newProjectServiceEntity = ProjectMembersFactory.CreateEntity(projectId, serviceId);
                await _projectMemberRepository.AddAsync(newProjectServiceEntity);
                var psAddSaveResult = await _projectMemberRepository.SaveAsync();
                if (psAddSaveResult == false)
                    throw new Exception("Error saving updated ProjectService");
            }

            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting ProjectService :: {ex.Message}");
        }
    }
}