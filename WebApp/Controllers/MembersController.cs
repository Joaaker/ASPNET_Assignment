using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Members";
            return View();
        }
    }
}
