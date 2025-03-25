namespace Domain.Models; 

public class Member
{
    public string Id { get; set; } = null!;
    //Member Image??
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? JobTitle{ get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public int? MemberAddressId { get; set; }
}