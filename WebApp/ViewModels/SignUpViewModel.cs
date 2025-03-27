using System.ComponentModel.DataAnnotations;
using Domain.Dtos;

namespace WebApp.ViewModels;
public class SignUpViewModel
{
    [Display(Name = "First Name", Prompt = "Your first name")]
    [Required(ErrorMessage = "Required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Your last name")]
    [Required(ErrorMessage = "Required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Invalid")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Compare(nameof(Password), ErrorMessage = "Invalid")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Terms and Conditions", Prompt = "I accept the terms and conditions")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions to use this site.")]
    public bool TermsAndConditions { get; set; }

   


    public static implicit operator MemberRegistrationFormDto(SignUpViewModel model)
    {
        return model == null
            ? null!
            : new MemberRegistrationFormDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleName = "User",
            };
    }
}