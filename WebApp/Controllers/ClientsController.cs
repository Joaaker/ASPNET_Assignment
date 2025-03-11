using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Clients";
            return View();
        }
    }
}
