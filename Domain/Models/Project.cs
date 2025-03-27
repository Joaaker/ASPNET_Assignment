namespace Domain.Models;

public class Project
{
    public int Id { get; set; }
    //Project Image? 
    //public IFormFile? ProjectImage { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? Budget { get; set; }
    public string ClientName { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public ICollection<Member> Members { get; set; } = [];
}