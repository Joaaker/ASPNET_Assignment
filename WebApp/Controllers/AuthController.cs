using Microsoft.AspNetCore.Mvc;
using Business.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign up";

            var formData = new SignUpFormModel();
            return View(formData);
        }

        [HttpPost]
        public IActionResult SignUp(SignUpFormModel formData)
        {
            if (!ModelState.IsValid)
                return View(formData);

            return View();
        }



        [Route("sign-in")]
        public IActionResult SignIn()
        {
            ViewData["Title"] = "Sign in";
            return View();
        }
        public new IActionResult SignOut()
        {
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
