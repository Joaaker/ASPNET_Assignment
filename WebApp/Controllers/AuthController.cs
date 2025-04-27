using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using WebApp.ViewModels;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Data.Entities;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService, IMemberService memberService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager, IHubContext<NotificationHub> notificationHub, INotificationService notificationService) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly IMemberService _memberService = memberService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;
    private readonly INotificationService _notificationService = notificationService;

    #region Local Sign Up
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel form)
    {
        ViewBag.ErrorMessage = null;
        if (ModelState.IsValid)
        {
            MemberRegistrationFormDto dto = form;

            var result = await _memberService.CreateMemberAsync(dto);
            if (result.Success)
                return RedirectToAction("SignIn", "Auth");
        }

        ViewBag.ErrorMessage = "Something went wrong, try again.";
        return View(form);
    }

    [Route("terms-and-conditions")]
    public IActionResult TermsAndConditions()
    {
        return View();
    }
    #endregion


    #region Local Sign In

    [Route("sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }

    [Route("sign-in")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel form, string returnUrl = "~/Projects/Index")
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
    #endregion

    #region External Authentication

    [HttpPost]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "Invalid provider");
            return View("SignIn");
        }

        var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl })!;
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/Projects/Index");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from external provider: {remoteError}");
            return View("SignIn");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("SignIn");

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            try
            {
                firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!;
                lastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!;
            }
            catch { }

            string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
            string username = $"ext_{info.LoginProvider.ToLower()}_{email}";

            var user = new MemberEntity { UserName = username, Email = email, FirstName = firstName, LastName = lastName, ImageUri = "https://aspnetassignment.blob.core.windows.net/images/1d0e95a8-e947-4877-8857-c15de4e55a87.svg" };

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("SignIn");
        }
    }
    #endregion
    public async Task<IActionResult> LogOut()
    {
        await _authService.LogOutAsync();
        return RedirectToAction("SignIn", "Auth");
    }
}