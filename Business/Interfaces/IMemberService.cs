using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembers();

    Task<bool> CreateMemberAsync(MemberRegistrationFormDto signUpForm);
}
