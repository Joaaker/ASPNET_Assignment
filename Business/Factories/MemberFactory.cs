namespace Business.Factories;

public class MemberFactory
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public string? JobTitle { get; set; }
    public string? MemberAddress { get; set; }
    public string? DateOfBirth { get; set; }
}
