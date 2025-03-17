namespace Business.Models;

public class AddProjectForm
{
    public string ProjectName { get; set; } = null!;
    public string? ClientName { get; set; }
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int MemberId { get; set; }
    public int Budget { get; set; }

}
