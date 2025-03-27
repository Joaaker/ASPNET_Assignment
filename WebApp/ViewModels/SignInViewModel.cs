using System.ComponentModel.DataAnnotations;
using Domain.Dtos;

namespace WebApp.ViewModels;

public class SignInViewModel
{
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

    public bool RememberMe { get; set; }

    public static implicit operator SignInDto(SignInViewModel model)
    {
        return model == null
            ? null!
            : new SignInDto
            {
                Email = model.Email.ToLower(),
                Password = model.Password,
            };
    }
}
