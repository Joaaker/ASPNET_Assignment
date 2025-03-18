using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AddMemberForm
{
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid")]
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string JobTitle { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
}
