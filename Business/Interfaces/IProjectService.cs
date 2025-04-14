using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<IResponseResult> CreateProjectAsync(ProjectRegistrationDto registrationForm);
    Task<IResponseResult<IEnumerable<Project>>> GetAllProjectsAsync();
    Task<IResponseResult> GetProjectByIdAsync(int id);
    Task<IResponseResult> UpdateProjectAsync(int id, ProjectRegistrationDto updateForm);
    Task<IResponseResult> DeleteProjectAsync(int id);
}