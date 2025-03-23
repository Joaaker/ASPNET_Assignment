using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    public IActionResult SignUp()
    {
        ViewData["Title"] = "Sign up";

        var formData = new SignUpForm();
        return View(formData);
    }

    [HttpPost]
    public IActionResult SignUp(SignUpForm formData)
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

    [Route("sign-in")]
    [HttpPost]
    public IActionResult SignIn(SignInForm form)
    {
        ViewData["Title"] = "Sign in";
        return View();
    }


    public new IActionResult SignOut()
    {
        return RedirectToAction("SignIn", "Auth");
    }
}
