using System.ComponentModel.DataAnnotations;
using Domain.Dtos;

namespace WebApp.ViewModels;

public class AddProjectViewModel
{
    [Display(Name = "Project Image", Prompt = "Select a image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Project name")]
    [Required(ErrorMessage = "Required")]
    public string Title { get; set; } = null!;

    [Display(Name = "Client")]
    [Required(ErrorMessage = "Required")]
    public int ClientId { get; set; }

    [Display(Name = "Description", Prompt = "Enter a description")]
    public string? Description { get; set; }

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Required")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "End Date")]
    [Required(ErrorMessage = "Required")]
    public DateOnly EndDate { get; set; }

    [Display(Name = "Members", Prompt = "Select project members")]
    public List<string> MembersIds { get; set; } = [];

    [Display(Name = "Project Status")]
    [Range(1, 3, ErrorMessage = "Required")]
    public int StatusId { get; set; }

    [Display(Name = "Budget", Prompt = "0")]
    public int Budget { get; set; }


    public static implicit operator ProjectRegistrationDto(AddProjectViewModel model)
    {
        return model == null
            ? null!
            : new ProjectRegistrationDto
            {
                Title = model.Title,
                ClientId = model.ClientId,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                MembersIds = model.MembersIds,
                Budget = model.Budget,
                StatusId = model.StatusId
            };
    }
}