using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationDto registrationForm);
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(int id);
    Task<bool> UpdateProjectAsync(int id, ProjectRegistrationDto updateForm);
    Task<bool> DeleteProjectAsync(int id);
}