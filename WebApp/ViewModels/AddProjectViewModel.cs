using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
    public IEnumerable<string> MembersIds { get; set; } = [];

    [Display(Name = "Budget", Prompt = "0")]
    public int Budget { get; set; }

}