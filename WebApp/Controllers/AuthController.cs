using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Business.Interfaces;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult SignUp()
    {
        var formData = new SignUpViewModel();
        return View(formData);
    }

    [HttpPost]
    public IActionResult SignUp(SignUpViewModel formData)
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

    //[Route("sign-in")]
    //[HttpPost]
    //public async Task<IActionResult> SignIn(SignInViewModel form)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        ViewBag.ErrorMessage = "Email or password is not correct";
    //        return View(form);
    //    }

    //    //Convert INTO DTO with IMPLICIT
    //    ////var result = await _authService.LogInAsync(form);
    //    //if (result)
    //    //    return RedirectToAction("Index", "Projects");
            
    //    return View();
    //}


    public new IActionResult SignOut()
    {
        return RedirectToAction("SignIn", "Auth");
    }
}
