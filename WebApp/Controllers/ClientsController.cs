using Business.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Controllers;

public class ClientsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Clients";

        return View();
    }

    [HttpPost]
    public IActionResult Add(AddClientForm formData)
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

    [HttpPost]
    public IActionResult Edit(AddClientForm formData)
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
