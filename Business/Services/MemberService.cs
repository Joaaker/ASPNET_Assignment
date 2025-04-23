using System.Diagnostics;
using System.Linq.Expressions;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager, IMemberAddressRepository memberAddressRepository, IMemberRepository memberRepsoitory) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly IMemberAddressRepository _memberAddressRepository = memberAddressRepository;
    private readonly IMemberRepository _memberRepository = memberRepsoitory;


    public async Task<IResponseResult<IEnumerable<Member>>> GetAllMembersAsync()
    {
        try
        {
            var memberEntities = await _userManager.Users.ToListAsync();
            var members = memberEntities.Select(MemberFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<Member>>.Ok(members);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult<IEnumerable<Member>>.Error("Error retrieving members");
        }
    }


    public async Task<IResponseResult> CreateMemberAsync(MemberRegistrationFormDto form)
    {
        if (form is null)
            return ResponseResult.BadRequest("Invalid form");

        if (form.RoleName != "Admin" && form.RoleName != "User")
            throw new Exception("Invalid role specified.");

        try
        {
            await _memberRepository.BeginTransactionAsync();

            var memberEntity = MemberFactory.CreateEntity(form);

            var userCreationResult = await _userManager.CreateAsync(memberEntity, form.Password ?? "BytMig123!");
            if (!userCreationResult.Succeeded)
                throw new Exception("Failed to create user");

            var addToRoleResult = await _userManager.AddToRoleAsync(memberEntity, form.RoleName);
            if (!addToRoleResult.Succeeded)
                throw new Exception("Failed to add role to user");

            if (!string.IsNullOrWhiteSpace(form.StreetName) &&
                !string.IsNullOrWhiteSpace(form.PostalCode) &&
                !string.IsNullOrWhiteSpace(form.City))
            {
                var memberAddressEntity = MemberAddressFactory.CreateEntity(form, memberEntity.Id);
                await _memberAddressRepository.AddAsync(memberAddressEntity);
                bool saveAddressResult = await _memberAddressRepository.SaveAsync();
                if (saveAddressResult == false)
                    throw new Exception("Failed to save member address.");
            }

            await _memberRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _memberRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error in CreateMemberAsync: {ex.Message}");
            return ResponseResult.Error("Error creating employee");
        }
    }

    public async Task<IResponseResult> GetMemberByExpressionAsync(Expression<Func<MemberEntity, bool>> expression)
    {
        try
        {
            var memberEntity = await _memberRepository.GetAsync(expression);
            if (memberEntity == null)
                return ResponseResult.Error("Member not found");
            
            var member = MemberFactory.CreateModel(memberEntity);
            return ResponseResult<Member>.Ok(member);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving member");
        }
    }

    public async Task<IResponseResult> DeleteMemberAsync(string memberId)
    {
        try
        {
            var memberEntity = await _memberRepository.GetAsync(x => x.Id == memberId);
            if (memberEntity == null)
                return ResponseResult.NotFound("Member not found");

            await _memberRepository.BeginTransactionAsync();

            await _memberRepository.DeleteAsync(x => x.Id == memberId);
            var saveResult = await _memberRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _memberRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _memberRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting member :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> UpdateMemberAsync(string memberId, MemberRegistrationFormDto updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        if (updateForm.RoleName != "Admin" && updateForm.RoleName != "User")
            throw new Exception("Invalid role specified.");

        try
        {
            var memberToUpdate = await _memberRepository.GetAsync(x => x.Id == memberId);
            if (memberToUpdate == null)
                return ResponseResult.NotFound("Member not found");

            await _memberRepository.BeginTransactionAsync();

            MemberFactory.UpdateMemberEntity(memberToUpdate, updateForm, memberId);
            await _memberRepository.UpdateAsync(x => x.Id == memberId, memberToUpdate);
            var saveResult = await _memberRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving updated project");

            if (!string.IsNullOrWhiteSpace(updateForm.StreetName) &&
               !string.IsNullOrWhiteSpace(updateForm.PostalCode) &&
               !string.IsNullOrWhiteSpace(updateForm.City) )
            {
                var memberAddress = await _memberAddressRepository.GetAsync(x => x.UserId == memberId);
                if(memberAddress.StreetName != updateForm.StreetName || 
                    memberAddress.PostalCode != updateForm.PostalCode || 
                    memberAddress.City != updateForm.City)
                {
                    MemberAddressFactory.UpdateMemberAddressEntity(memberAddress, updateForm, memberId);
                    await _memberAddressRepository.UpdateAsync(x => x.UserId == memberId, memberAddress);
                    bool saveAddressResult = await _memberAddressRepository.SaveAsync();
                    if (saveAddressResult == false)
                        throw new Exception("Failed to save member address.");
                }
            }

            await _memberRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _memberRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating project :: {ex.Message}");
        }
    }
}