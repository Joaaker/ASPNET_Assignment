using Business.Interfaces;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public Task<bool> CreateProjectAsync(ProjectRegistrationDto registrationForm)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProjectAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetProjectByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProjectAsync(int id, ProjectRegistrationDto updateForm)
    {
        throw new NotImplementedException();
    }
}
