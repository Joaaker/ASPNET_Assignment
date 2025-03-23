using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    [Route("admin-panel")]
    public IActionResult SignIn()
    {
        ViewData["Title"] = "Admin Sign In";
        return View();
    }
}
