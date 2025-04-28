namespace Domain.Dtos;

public class ProjectRegistrationDto 
{
    public string? ProjectImageUri { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? Budget { get; set; }
    public int StatusId { get; set; }
    public int ClientId { get; set; }
    public List<string> MembersIds { get; set; } = [];
}