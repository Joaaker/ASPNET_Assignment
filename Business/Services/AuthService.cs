using Business.Interfaces;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(SignInManager<MemberEntity> signInManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;

    public async Task<bool> LogInAsync(SignInForm signInForm)
    {
        var result = await _signInManager.PasswordSignInAsync(signInForm.Email, signInForm.Password, false, false);
        return result.Succeeded;
    }


}
