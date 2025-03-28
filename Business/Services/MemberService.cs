using System.Collections.Generic;
using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager, IMemberAddressRepository memberAddressRepository, IMemberRepository memberRepsoitory) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly IMemberAddressRepository _memberAddressRepository = memberAddressRepository;
    private readonly IMemberRepository _memberRepository = memberRepsoitory;


    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        var memberEntities = await _userManager.Users.ToListAsync();
        var members = memberEntities.Select(MemberFactory.CreateModel);

        return members;
    }


    public async Task<bool> CreateMemberAsync(MemberRegistrationFormDto signUpForm)
    {
        if (signUpForm is null)
            throw new Exception("Sign up form cannot be null.");

        if (signUpForm.RoleName != "Admin" && signUpForm.RoleName != "User")
            throw new Exception("Invalid role specified.");

        try
        {
            await _memberRepository.BeginTransactionAsync();

            var memberEntity = MemberFactory.CreateEntity(signUpForm);

            var userCreationResult = await _userManager.CreateAsync(memberEntity, signUpForm.Password ?? "BytMig123!");
            if (!userCreationResult.Succeeded)
                throw new Exception("Failed to create user");

            var addToRoleResult = await _userManager.AddToRoleAsync(memberEntity, signUpForm.RoleName);
            if (!addToRoleResult.Succeeded)
                throw new Exception("Failed to add role to user");

            if (!string.IsNullOrWhiteSpace(signUpForm.StreetName) &&
                !string.IsNullOrWhiteSpace(signUpForm.PostalCode) &&
                !string.IsNullOrWhiteSpace(signUpForm.City))
            {
                var memberAddressEntity = MemberAddressFactory.CreateEntity(signUpForm, memberEntity.Id);
                await _memberAddressRepository.AddAsync(memberAddressEntity);
                bool saveAddressResult = await _memberAddressRepository.SaveAsync();
                if (saveAddressResult == false)
                    throw new Exception("Failed to save member address.");
            }

            await _memberRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _memberRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error in CreateMemberAsync: {ex.Message}");
            return false;
        }
    }
}