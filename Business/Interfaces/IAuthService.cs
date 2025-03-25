using Business.Dtos;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<bool> LogInAsync(SignInDto signInForm);
}