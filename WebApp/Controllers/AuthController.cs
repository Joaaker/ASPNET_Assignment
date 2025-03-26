using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using WebApp.ViewModels;
using Domain.Dtos;


namespace WebApp.Controllers;

public class AuthController(IAuthService authService, IMemberService memberService) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly IMemberService _memberService = memberService;

    public IActionResult SignUp()
    {
        var formData = new SignUpViewModel();
        return View(formData);
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel form)
    {
        if (ModelState.IsValid)
        {
            MemberRegistrationFormDto dto = form;

            var result = await _memberService.CreateMemberAsync(dto);
            if (result)
                return RedirectToAction("Index", "Projects");
        }

        return View(form);
    }



    [Route("sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }


    [Route("sign-in")]
    [HttpPost]
    public async Task<IActionResult> Login(SignInViewModel form, string returnUrl = "~/Projects/Index")
    {
        ViewBag.ErrorMessage = null;

        if (ModelState.IsValid)
        {
            SignInDto dto = form;
            var result = await _authService.LogInAsync(dto);
            if (result)
                return Redirect(returnUrl);
        }

        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(form);
    }



    [Route("terms-and-conditions")]
    public IActionResult TermsAndConditions()
    {
        return View();
    }

    public new IActionResult SignOut()
    {
        return RedirectToAction("SignIn", "Auth");
    }
}
