using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<IResponseResult> GetAllMembers();

    Task<IResponseResult> CreateMemberAsync(MemberRegistrationFormDto signUpForm);

    Task<IResponseResult> GetMemberById(string id);
}
