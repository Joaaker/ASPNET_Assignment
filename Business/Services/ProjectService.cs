using Business.Factories;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Data.Entities;
using System.Linq.Expressions;

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

            if (form.MembersIds.Count != 0)
            {
                foreach (string memberIds in form.MembersIds)
                {
                    var projectMemberEntity = ProjectMembersFactory.CreateEntity(newProject.Id, memberIds);
                    await _projectMemberRepository.AddAsync(projectMemberEntity);
                }

                var pmSaveResult = await _projectMemberRepository.SaveAsync();
                if (pmSaveResult == false)
                    throw new Exception("Error saving project members");
            }


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

    public async Task<IResponseResult<IEnumerable<Project>>> GetAllProjectsAsync()
    {
        try
        {
            var entities = await _projectRepository.GetAllAsync();

            var projects = entities.Select(e => ProjectFactory.CreateModel(e));

            return ResponseResult<IEnumerable<Project>>.Ok(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult<IEnumerable<Project>>.Error("Error retrieving projects");
        }
    }

    public async Task<IResponseResult> GetProjectByExpressionAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        try
        {
            var projectEntity = await _projectRepository.GetAsync(expression);

            var project = ProjectFactory.CreateModel(projectEntity);

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

        var projectToUpdate = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectToUpdate == null)
            return ResponseResult.NotFound("Project not found");

        bool projectNeedsUpdate = updateForm.ProjectImageUri != null || projectToUpdate.Title != updateForm.Title || projectToUpdate.Description != updateForm.Description || projectToUpdate.StartDate != updateForm.StartDate || projectToUpdate.EndDate != updateForm.EndDate || projectToUpdate.Budget != updateForm.Budget || projectToUpdate.ClientId != updateForm.ClientId || projectToUpdate.StatusId != updateForm.StatusId;

        try 
        {
            
            if (projectNeedsUpdate)
            {
                await _projectRepository.BeginTransactionAsync();
                ProjectFactory.UpdateEntity(projectToUpdate, updateForm);
                await _projectRepository.UpdateAsync(x => x.Id == id, projectToUpdate);
                var saveResult = await _projectRepository.SaveAsync();
                if (saveResult == false)
                    throw new Exception("Error saving updated project");

                await _projectRepository.CommitTransactionAsync();
            }

            var existingMemberIds = projectToUpdate.ProjectMembers
                .Select(junctionTable => junctionTable.UserId)
                .ToList();

            if (updateForm.MembersIds != null && existingMemberIds != null)
            {
                var projectMembersUpdate = await _projectMemberService.UpdateProjectMembersAsync(id, existingMemberIds, updateForm.MembersIds);
                if (projectMembersUpdate.Success == false)
                    throw new Exception("Error updating ProjectServices");
            }
            
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
