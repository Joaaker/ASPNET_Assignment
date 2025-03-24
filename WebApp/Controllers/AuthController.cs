using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Business.Interfaces;
using System.Threading.Tasks;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult SignUp()
    {
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
        return View();
    }

    [Route("sign-in")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInForm form)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Email or password is not correct";
            return View(form);
        }

        var result = await _authService.LogInAsync(form);
        if (result)
            return RedirectToAction("Index", "Projects");
            
        return View();
    }


    public new IActionResult SignOut()
    {
        return RedirectToAction("SignIn", "Auth");
    }
}
