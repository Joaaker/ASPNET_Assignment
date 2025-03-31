using Business.Interfaces;
using Business.Models;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    //public async Task<IActionResult> Index()
    //{
    //    var result = await _memberService.GetAllMembers();
    //    if (!result.Success)
    //        return View("Error", result.ErrorMessage);

    //    var members = ((ResponseResult<IEnumerable<Member>>)result).Data ?? [];
    //    IEnumerable<MembersViewModel> model = [.. members.Select(m => (MembersViewModel)m)];
    //    return View(model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Add(AddMemberViewModel form)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        var errors = ModelState
    //            .Where(x => x.Value?.Errors.Count > 0)
    //            .ToDictionary(
    //                kvp => kvp.Key,
    //                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
    //                );

    //        return BadRequest(new { success = false, errors });
    //    }

    //    MemberRegistrationFormDto dto = form;

    //    var result = await _memberService.CreateMemberAsync(dto);
    //    if (!result.Success)
    //        return StatusCode(result.StatusCode, new { success = false, message = result.ErrorMessage });


    //    return Ok(new { success = true });
    //}
    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProjectViewModel formData)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

            return BadRequest(new { success = false, errors });
        }

        ProjectRegistrationDto dto = formData;

        var result = await _projectService.CreateProjectAsync(dto);
        if (!result.Success)
            return StatusCode(result.StatusCode, new { success = false, message = result.ErrorMessage });

        return Ok(new { success = true });
    }

    [HttpPost]
    public IActionResult Edit(Project formData)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

            return BadRequest(new { success = false, errors });
        }

        //Send Data to Service
        return Ok(new { success = true });
    }
}
