using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class AddProjectForm
{
    [Display(Name = "Project Image", Prompt = "Select a image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Project name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;


    [Display(Name = "Description", Prompt = "Type something")]
    public string? Description { get; set; }


    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Required")]
    public DateOnly StartDate { get; set; }


    [Display(Name = "End Date")]
    public DateOnly? EndDate { get; set; }


    [Display(Name = "Budget", Prompt = "0")]
    public int Budget { get; set; }


    [Display(Name = "Client Name", Prompt = "Select a client")]
    [Required(ErrorMessage = "Required")]
    public int ClientId { get; set; }


    [Display(Name = "Members", Prompt = "Select project members")]
    public List<int> MembersIds { get; set; } = [];
}