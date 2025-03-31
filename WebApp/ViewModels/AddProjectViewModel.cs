using System.ComponentModel.DataAnnotations;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AddProjectViewModel
{
    [Display(Name = "Project Image", Prompt = "Select a image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Project name")]
    [Required(ErrorMessage = "Required")]
    public string Title { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Select a client")]
    [Required(ErrorMessage = "Required")]
    public int ClientId { get; set; }

    [Display(Name = "Description", Prompt = "Type a description")]
    public string? Description { get; set; }

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Required")]
    public DateOnly StartDate { get; set; }


    [Display(Name = "End Date")]
    [Required(ErrorMessage = "Required")]
    public DateOnly EndDate { get; set; }

    [Display(Name = "Members", Prompt = "Select project members")]
    public List<string> MembersIds { get; set; } = [];

    [Display(Name = "Budget", Prompt = "0")]
    public int Budget { get; set; }

    //[Display(Name = "Project Status", Prompt = "Select a project status")]
    //public List<SelectListItem> StatusList { get; set; }

    [Display(Name = "Selected Status", Prompt = "Select a project status")]
    [Range(1, 3, ErrorMessage = "Required")]
    public int StatusId { get; set; }



    public static implicit operator ProjectRegistrationDto(AddProjectViewModel model)
    {
        return model == null
            ? null!
            : new ProjectRegistrationDto
            {
                //Project Image?
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

    //StatusId = int.TryParse(model.StatusId, out int statusId) ? statusId : 1


    //public AddProjectViewModel() => StatusList = [
    //        new() { Value = "1", Text = "Not started" },
    //        new() { Value = "2", Text = "Started" },
    //        new() { Value = "3", Text = "Completed" }
    //    ];
}