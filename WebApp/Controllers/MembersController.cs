using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
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
        var result = await _memberService.GetAllMembers();
        if (!result.Success)
        {
            // Hantera fel, exempelvis genom att visa en felvy med felmeddelande
            return View("Error", result.ErrorMessage);
        }

        // Vi antar att result är av typen ResponseResult<IEnumerable<Member>>
        var members = ((ResponseResult<IEnumerable<Member>>)result).Data;
        // Använder den implicita operatorn för att konvertera varje Member till en MembersViewModel
        IEnumerable<MembersViewModel> model = members.Select(m => (MembersViewModel)m).ToList();
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Add(AddMemberViewModel form)
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
    public IActionResult Edit(EditMemberViewModel formData)
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
