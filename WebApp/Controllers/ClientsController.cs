using Business.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class ClientsController(IClientService clientService) : Controller
{
    private readonly IClientService _clientService = clientService;

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

        ClientRegistrationDto registrationDto = formData;

        var result = await _clientService.CreateClientAsync(registrationDto);

        if (!result.Success)
            return StatusCode(result.StatusCode, new { success = false, message = result.ErrorMessage });
        

        return Ok(new { success = true });
    }

    [HttpPost]
    public IActionResult Edit(ClientsViewModel formData)
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
