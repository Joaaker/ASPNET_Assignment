using Data.Entities;
using System.Linq.Expressions;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<IResponseResult> GetAllMembers();

    Task<IResponseResult> CreateMemberAsync(MemberRegistrationFormDto signUpForm);

    Task<IResponseResult> GetMemberByExpression(Expression<Func<MemberEntity, bool>> predicate);
}
