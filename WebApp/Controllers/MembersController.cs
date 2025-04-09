using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class MembersController(IMemberService memberService, IHubContext<NotificationHub> notficationHub, INotificationService notificationService) : Controller
{
    private readonly IMemberService _memberService = memberService;
    private readonly IHubContext<NotificationHub> _notificationHub = notficationHub;
    private readonly INotificationService _notificationService = notificationService;

    public async Task<IActionResult> Index()
    {
        var result = await _memberService.GetAllMembers();
        if (!result.Success)
            return View("Error", result.ErrorMessage);
        
        var members = ((ResponseResult<IEnumerable<Member>>)result).Data ?? [];
        IEnumerable<MembersViewModel> model = [.. members.Select(m => (MembersViewModel)m)];
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

        var createResult = await _memberService.CreateMemberAsync(dto);
        if (createResult.Success)
        {
            if (await _memberService.GetMemberByExpression(x => x.Email == form.Email) is IResponseResult<Member> memberResult && memberResult.Data != null)
            {
                var member = memberResult.Data;
                var message = $"{member.FirstName} {member.LastName} added";
                var notificationEntity = NotificationFactory.CreateDto(2, 1, message, null);

                await _notificationService.AddNotificationAsync(notificationEntity);
            }
            return Ok(new { success = true });
        } else
            return StatusCode(createResult.StatusCode, new { success = false, message = createResult.ErrorMessage });
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

    [HttpGet]
    public async Task<IActionResult> GetMember(string id)
    {
        var result = await _memberService.GetMemberByExpression(x => x.Id == id);

        var member = ((ResponseResult<Member>)result).Data;

        return Json(member);
    }
}
