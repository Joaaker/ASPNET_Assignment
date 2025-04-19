using Domain.Models;

namespace WebApp.ViewModels;

public class ProjectViewModel
{
    public IEnumerable<Project> Project { get; set; } = [];
}
