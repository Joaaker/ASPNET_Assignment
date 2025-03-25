namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    //Project Image? 
    //public IFormFile? ProjectImage { get; set; }
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? Budget { get; set; }
    public string ClientName { get; set; } = null!;
    public ICollection<int> MembersIds { get; set; } = [];
}