using Data.Entities;
using Domain.Dtos;
using Domain.Models;

namespace Business.Factories;

public class ProjectFactory
{
    public static ProjectEntity CreateEntity(ProjectRegistrationDto registrationForm) => new()
    {
        Title = registrationForm.Title,
        Description = registrationForm.Description,
        StartDate = registrationForm.StartDate,
        EndDate = registrationForm.EndDate,
        StatusId = registrationForm.StatusId,
        ClientId = registrationForm.ClientId,
        Budget = registrationForm.Budget
    };

    public static Project CreateModel(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        StatusName = entity.Status.StatusName,
        ClientName = entity.Client.ClientName,
        ProjectMembers = [.. entity.ProjectMembers
        .Select(junctionTable => new Member
        {
            Id = junctionTable.UserId,
            FirstName = junctionTable.Member.FirstName,
            LastName = junctionTable.Member.LastName,
        })]
    };
    public static void UpdateEntity(ProjectEntity existingProject, ProjectRegistrationDto updateForm)
    {
        existingProject.Title = updateForm.Title;
        existingProject.Description = updateForm.Description;
        existingProject.StartDate = updateForm.StartDate;
        existingProject.EndDate = updateForm.EndDate;
        existingProject.StatusId = updateForm.StatusId;
        existingProject.ClientId = updateForm.ClientId;
        existingProject.Budget = updateForm.Budget;
    }
}
