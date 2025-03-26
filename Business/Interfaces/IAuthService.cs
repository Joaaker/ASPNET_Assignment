using Domain.Dtos;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<bool> LogInAsync(SignInDto signInForm);
}