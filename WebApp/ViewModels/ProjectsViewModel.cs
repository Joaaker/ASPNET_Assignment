using Domain.Models;

namespace WebApp.ViewModels;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];
}