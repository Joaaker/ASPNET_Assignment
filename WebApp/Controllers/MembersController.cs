using System.Threading.Tasks;
using Business.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class MembersController(IMemberService memberService) : Controller
{
    private readonly IMemberService _memberService = memberService;
    public async Task<IActionResult> Index()
    {
        // Anropa service-lagret och hämta response-resultatet.
        var members = await _memberService.GetAllMembers();

        if (!response.Success)
        {
            // Hantera fel, exempelvis genom att lägga till ett modellfel eller visa en felvy.
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            // Här kan du exempelvis returnera en tom lista eller en särskild felvy.
            return View(new List<MembersViewModel>());
        }

  

        // Mappa varje Member till en MembersViewModel med hjälp av den implicita operatorn.
        var membersViewModels = members.Select(m => (MembersViewModel)m).ToList();

        return View(membersViewModels);
    }

    [HttpPost]
    public async Task<IActionResult> Add(MembersViewModel form)
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

        MemberRegistrationFormDto dto = form;

        var result = await _memberService.CreateMemberAsync(dto);
        if (!result.Success)
            return StatusCode(result.StatusCode, new { success = false, message = result.ErrorMessage });


        return Ok(new { success = true });
    }

    [HttpPost]
    public IActionResult Edit(MembersViewModel formData)
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
