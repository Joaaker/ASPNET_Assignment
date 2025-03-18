using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ClientsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Clients";

        return View();
    }

    [HttpPost]
    public IActionResult Index(AddClientForm formData)
    {
        if (!ModelState.IsValid)
            return View();

        return View();
    }
}
