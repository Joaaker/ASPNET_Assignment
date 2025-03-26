namespace Domain.Dtos;

public class MemberRegistrationDto
{
    //Member Image??
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? JobTitle { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? MemberAddress { get; set; }
}