using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class AddClientViewModel
{
    [Display(Name = "Client Name", Prompt = "Client name")]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Client email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone", Prompt = "Client phone number")]
    [Required(ErrorMessage = "Required")]
    public string PhoneNumber { get; set; } = null!;
}