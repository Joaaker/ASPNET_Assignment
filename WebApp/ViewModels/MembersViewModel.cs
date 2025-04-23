using Domain.Models;

namespace WebApp.ViewModels;

public class MembersViewModel
{
    public string? Id { get; set; }

    public string? ImageUri { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; } = null!;

    public string? JobTitle { get; set; } = null!;


    public static implicit operator MembersViewModel(Member model)
    {
        return model == null
            ? null!
            : new MembersViewModel
            {
                Id = model.Id,
                ImageUri = model.ImageUri,
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Email = model.Email!,
                PhoneNumber = model.PhoneNumber,
                JobTitle = model.JobTitle,
            };
    }
}
