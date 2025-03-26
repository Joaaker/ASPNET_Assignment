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
            return false;

        await _memberRepository.BeginTransactionAsync();

        try
        {
            if (!string.IsNullOrWhiteSpace(signUpForm.StreetName) &&
                !string.IsNullOrWhiteSpace(signUpForm.PostalCode) &&
                !string.IsNullOrWhiteSpace(signUpForm.City))
            {
                var memberAddressEntity = MemberAddressFactory.CreateEntity(signUpForm);
                await _memberAddressRepository.AddAsync(memberAddressEntity);

                bool saveAddressResult = await _memberAddressRepository.SaveAsync();
                if (saveAddressResult == false)
                    throw new InvalidOperationException("Failed to save member address.");
            }

            var memberEntity = MemberFactory.CreateEntity(signUpForm);

            var userCreationResult = await _userManager.CreateAsync(memberEntity, signUpForm.Password ?? "BytMig123!");
            if (!userCreationResult.Succeeded)
            {
                var errors = string.Join(", ", userCreationResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user: {errors}");
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