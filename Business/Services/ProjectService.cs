using Business.Factories;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IProjectMemberRepository projectMemberRepository, IProjectMemberService projectMemberService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository = projectMemberRepository;
    private readonly IProjectMemberService _projectMemberService = projectMemberService;

    public async Task<IResponseResult> CreateProjectAsync(ProjectRegistrationDto form)
    {
        if (form == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var projectExist = await _projectRepository.AlreadyExistsAsync(x => x.Title == form.Title);
            if (projectExist == true)
                return ResponseResult.Error("Project with that name already exist");

            await _projectRepository.BeginTransactionAsync();
            var newProject = ProjectFactory.CreateEntity(form);
            await _projectRepository.AddAsync(newProject);
            var saveResult = await _projectRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving project");

            foreach (string memberIds in form.MembersIds)
            {
                var projectMemberEntity = ProjectMembersFactory.CreateEntity(newProject.Id, memberIds);
                await _projectMemberRepository.AddAsync(projectMemberEntity);
            }

            var psSaveResult = await _projectRepository.SaveAsync();
            if (psSaveResult == false)
                throw new Exception("Error saving ProjectService");

            await _projectRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error creating project :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> DeleteProjectAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Project not found");

            await _projectRepository.BeginTransactionAsync();
            await _projectRepository.DeleteAsync(x => x.Id == id);
            var saveResult = await _projectRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving project");

            await _projectRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting project :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> GetAllProjectsAsync()
    {
        try
        {
            var entites = await _projectRepository.GetAllAsync();
            var projects = entites.Select(ProjectFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<Project>>.Ok(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving projects");
        }
    }

    public async Task<IResponseResult> GetProjectByIdAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Project not found");

            var project = ProjectFactory.CreateModel(entity);
            return ResponseResult<Project>.Ok(project);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving project");
        }
    }

    public async Task<IResponseResult> UpdateProjectAsync(int id, ProjectRegistrationDto updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var projectToUpdate = await _projectRepository.GetAsync(x => x.Id == id);
            if (projectToUpdate == null)
                return ResponseResult.NotFound("Project not found");

            await _projectRepository.BeginTransactionAsync();
            ProjectFactory.UpdateEntity(projectToUpdate, updateForm);
            await _projectRepository.UpdateAsync(x => x.Id == id, projectToUpdate);
            var saveResult = await _projectRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving updated project");

            var existingMemberIds = projectToUpdate.ProjectMembers
                .Select(junctionTable => junctionTable.UserId)
                .ToList();

            var projectServicesUpdate = _projectMemberService.UpdateProjectServicesAsync(
                id, existingMemberIds, updateForm.MembersIds);
            if (projectServicesUpdate.Result.Success == false)
                throw new Exception("Error updating ProjectServices");

            await _projectRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating project :: {ex.Message}");
        }
    }
}
