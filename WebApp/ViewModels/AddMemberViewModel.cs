using System.ComponentModel.DataAnnotations;
using Domain.Dtos;


namespace WebApp.ViewModels;

public class AddMemberViewModel
{
    [Display(Name = "Member Image", Prompt = "Select a image")]
    [DataType(DataType.Upload)]
    public IFormFile? MemberImage { get; set; }

    [Display(Name = "First Name", Prompt = "Enter first name")]
    [Required(ErrorMessage = "Required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter last name")]
    [Required(ErrorMessage = "Required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Enter email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone", Prompt = "Enter phone number")]
    [Required(ErrorMessage = "Required")]
    public string PhoneNumber { get; set; } = null!;

    [Display(Name = "Job Title", Prompt = "Enter job title")]
    [Required(ErrorMessage = "Required")]
    public string JobTitle { get; set; } = null!;

    [Display(Name = "Street Name", Prompt = "Enter street name")]
    [Required(ErrorMessage = "Required")]
    public string StreetName { get; set; } = null!;

    [Display(Name = "Postal Code", Prompt = "Enter postal code")]
    [Required(ErrorMessage = "Required")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter city")]
    [Required(ErrorMessage = "Required")]
    public string City { get; set; } = null!;

    [Display(Name = "Date of Birth")]
    [Required(ErrorMessage = "Required")]
    public DateOnly DateOfBirth { get; set; }

    [Display(Name = "Account Role")]
    [Required(ErrorMessage = "Required")]
    public string RoleName { get; set; } = null!;


    public static implicit operator MemberRegistrationFormDto(AddMemberViewModel model)
    {
        return model == null
            ? null!
            : new MemberRegistrationFormDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                JobTitle = model.JobTitle,
                StreetName = model.StreetName,
                PostalCode = model.PostalCode,
                City = model.City,
                DateOfBirth = model.DateOfBirth,
                RoleName = model.RoleName
            };
    }
}