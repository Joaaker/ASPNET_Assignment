using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Domain.Dtos;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class ClientsController(IClientService clientService, INotificationService notificationService) : Controller
{
    private readonly IClientService _clientService = clientService;
    private readonly INotificationService _notificationService = notificationService;

    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddClientViewModel formData)
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

        var dto = formData.MapTo<ClientRegistrationDto>();

        var createResult = await _clientService.CreateClientAsync(dto);
        if (createResult.Success)
        {
            if (await _clientService.GetClientByExpressionAsync(x => x.Email == formData.Email) is IResponseResult<Client> clientResult && clientResult.Data != null)
            {
                var client = clientResult.Data;
                var message = $"{client.ClientName} added";
                var notificationEntity = NotificationFactory.CreateDto(2, 1, message, null);

                await _notificationService.AddNotificationAsync(notificationEntity);
            }
        }
        return createResult.Success
            ? Ok(new { success = true })
            : StatusCode(createResult.StatusCode, new { success = false, message = createResult.ErrorMessage });
    }

    [HttpGet]
    public async Task<IActionResult> GetClient(int id)
    {
        var result = await _clientService.GetClientByExpressionAsync(x => x.Id == id);

        var client = ((ResponseResult<Client>)result).Data;

        return Json(client);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditClientViewModel formData)
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

        var dto = formData.MapTo<ClientRegistrationDto>();

        var updateResult = await _clientService.UpdateClientAsync(formData.Id, dto);

        return updateResult.Success
            ? Ok(new { success = true })
            : StatusCode(updateResult.StatusCode, new { success = false, message = updateResult.ErrorMessage });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _clientService.DeleteClientAsync(id);
        if (!deleteResult.Success)
            return StatusCode(deleteResult.StatusCode, new { success = false, message = deleteResult.ErrorMessage });

        return RedirectToAction("Index");
    }
}
