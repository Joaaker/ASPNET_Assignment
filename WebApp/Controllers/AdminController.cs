using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    [Route("admin-panel")]
    public IActionResult AdminSignIn()
    {
        return View();
    }
}