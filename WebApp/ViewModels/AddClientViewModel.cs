using System.ComponentModel.DataAnnotations;
using Domain.Dtos;

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
    public string Phone { get; set; } = null!;


    public static implicit operator ClientRegistrationDto(AddClientViewModel model)
    {
        return model == null
            ? null!
            : new ClientRegistrationDto
            {
                ClientName = model.ClientName,
                Email = model.Email,
                Phone = model.Phone
            };
    }
}