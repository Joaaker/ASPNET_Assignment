using Domain.Models;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LogInAsync(SignInForm signInForm);
    }
}