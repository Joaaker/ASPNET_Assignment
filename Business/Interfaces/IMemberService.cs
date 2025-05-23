﻿using Data.Entities;
using System.Linq.Expressions;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<IResponseResult<IEnumerable<Member>>> GetAllMembersAsync();
    Task<IResponseResult> CreateMemberAsync(MemberRegistrationFormDto form);
    Task<IResponseResult> GetMemberByExpressionAsync(Expression<Func<MemberEntity, bool>> predicate);
    Task<IResponseResult> DeleteMemberAsync(string memberId);
    Task<IResponseResult> UpdateMemberAsync(string memberId, MemberRegistrationFormDto updateForm);
    Task<IResponseResult<IEnumerable<Member>>> SearchMembersAsync(string term);
}