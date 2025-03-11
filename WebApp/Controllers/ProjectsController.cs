using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Projects";
            return View();
        }
    }
}
