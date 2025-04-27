using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Domain.Dtos;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class MembersController(IMemberService memberService, IHubContext<NotificationHub> notficationHub, INotificationService notificationService, IFileHandler fileHandler) : Controller
{
    private readonly IMemberService _memberService = memberService;
    private readonly IHubContext<NotificationHub> _notificationHub = notficationHub;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IFileHandler _fileHandler = fileHandler;

    public async Task<IActionResult> Index()
    {
        var result = await _memberService.GetAllMembersAsync();
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
        var imageFileUri = await _fileHandler.UploadFileAsync(form.MemberImage!);

        MemberRegistrationFormDto dto = form;

        dto.ImageUri = imageFileUri;

        var createResult = await _memberService.CreateMemberAsync(dto);
        if (createResult.Success)
        {
            if (await _memberService.GetMemberByExpressionAsync(x => x.Email == form.Email) is IResponseResult<Member> memberResult && memberResult.Data != null)
            {
                var member = memberResult.Data;
                var notificationDto = NotificationFactory.CreateDto(2, 1, $"{member.FirstName} {member.LastName} added", member.ImageUri);


                var notificationResult = await _notificationService.AddNotificationAsync(notificationDto);
                if (notificationResult.Success && notificationResult.Data != null)
                {
                    var notificationEntity = notificationResult.Data;
                    await _notificationHub.Clients.Group("Admins").SendAsync("SendNotification", notificationEntity);
                }
            }
        } 
        return createResult.Success
            ? Ok(new { success = true })
            : StatusCode(createResult.StatusCode, new { success = false, message = createResult.ErrorMessage });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditMemberViewModel formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { success = false, message = "ModelState not valid." });

        var imageFileUri = await _fileHandler.UploadFileAsync(formData.MemberImage!);

        MemberRegistrationFormDto dto = formData;

        dto.ImageUri = imageFileUri;

        var updateResult = await _memberService.UpdateMemberAsync(formData.Id, dto);

        var notificationMessage = updateResult.Success
            ? $"{formData.FirstName} {formData.LastName} successfully updated."
            : "Error occurred while updating a member.";

        var notificationDto = NotificationFactory.CreateDto(2, 1, notificationMessage, dto.ImageUri);
        var notificationResult = await _notificationService.AddNotificationAsync(notificationDto);
        if (notificationResult.Success && notificationResult.Data != null)
        {
            var notificationEntity = notificationResult.Data;
            await _notificationHub.Clients.Group("Admins").SendAsync("SendNotification", notificationEntity);
        }

        return updateResult.Success
            ? Ok(new { success = true })
            : StatusCode(updateResult.StatusCode, new { success = false, message = updateResult.ErrorMessage });
    }

    [HttpGet]
    public async Task<IActionResult> GetMember(string id)
    {
        var result = await _memberService.GetMemberByExpressionAsync(x => x.Id == id);
        var member = ((ResponseResult<Member>)result).Data;
        return Json(member);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest(new { success = false, message = "Invalid member id." });

        var memberResponse = await _memberService.GetMemberByExpressionAsync(x => x.Id == id);
        if (memberResponse is not IResponseResult<Member> memberResult || memberResult.Data == null)
            return NotFound(new { success = false, message = "Member not found." });
            
        var member = memberResult.Data;

        var deleteResult = await _memberService.DeleteMemberAsync(id);

        string notificationMessage = deleteResult.Success
            ? $"{member.FirstName} {member.LastName} successfully deleted."
            : $"Error deleting {member.FirstName} {member.LastName}.";

        var notificationDto = NotificationFactory.CreateDto(2, 1, notificationMessage, member.ImageUri);
        var notificationResult = await _notificationService.AddNotificationAsync(notificationDto);
        if (notificationResult.Success && notificationResult.Data != null)
        {
            var notificationEntity = notificationResult.Data;
            await _notificationHub.Clients.Group("Admins").SendAsync("SendNotification", notificationEntity);
        }

        return deleteResult.Success
            ? Ok(new { success = true })
            : StatusCode(deleteResult.StatusCode, new { success = false, message = deleteResult.ErrorMessage });
    }

    [HttpGet]
    public async Task<JsonResult> SearchUsers(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return Json(new List<object>());

        //var users = await _context.Users
        //    .Where(x => x.FirstName.Contains(term) || x.LastName.Contains(term) || x.Email.Contains(term))
        //    .Select(x => new { x.Id, x.ImageUrl, FullName = x.FirstName + " " + x.LastName })
        //    .ToListAsync();
        await Task.Delay(5000); 

        //return Json(users);
        return Json(new List<object>());
    }
}