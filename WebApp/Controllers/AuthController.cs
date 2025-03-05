using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign up";
            return View();
        }

        [Route("sign-in")]
        public IActionResult SignIn()
        {
            ViewData["Title"] = "Sign in";
            return View();
        }
        public IActionResult AuthSignOut()
        {
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
