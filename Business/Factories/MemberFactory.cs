using Data.Entities;
using Domain.Dtos;
using Domain.Models;

namespace Business.Factories;

public class MemberFactory
{
    public static MemberEntity CreateEntity(MemberRegistrationFormDto registrationForm) => new()
    {
        UserName = registrationForm.Email,
        Email = registrationForm.Email,
        PhoneNumber = registrationForm.PhoneNumber,
        FirstName = registrationForm.FirstName,
        LastName = registrationForm.LastName,
        JobTitle = registrationForm.JobTitle,
        DateOfBirth = registrationForm.DateOfBirth,
    };

    public static Member CreateModel(MemberEntity memberEntity) => new()
    {
        Id = memberEntity.Id,
        Email = memberEntity.Email!,
        FirstName = memberEntity.FirstName,
        LastName = memberEntity.LastName,
        PhoneNumber = memberEntity.PhoneNumber,
        JobTitle = memberEntity.JobTitle,
        DateOfBirth = memberEntity.DateOfBirth,
        StreetName = memberEntity.Address?.StreetName,
        PostalCode = memberEntity.Address?.PostalCode,
        City = memberEntity.Address?.City
    };

    public static void UpdateMemberEntity(MemberEntity currentEntity, MemberRegistrationFormDto updateForm, string memberId)
    {
        currentEntity.Id = memberId;
        currentEntity.Email = updateForm.Email;
        currentEntity.FirstName = updateForm.FirstName;
        currentEntity.LastName = updateForm.LastName;
        currentEntity.PhoneNumber = updateForm.PhoneNumber;
        currentEntity.DateOfBirth = updateForm.DateOfBirth;
        currentEntity.JobTitle = updateForm.JobTitle;
    }
}