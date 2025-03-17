namespace Business.Models;

public class AddMemberForm
{
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string JobTitle { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
}
