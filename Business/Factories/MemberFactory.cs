using Data.Entities;
using Domain.Dtos;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Business.Factories;

public class MemberFactory
{
    public static MemberEntity CreateEntity(MemberRegistrationFormDto registrationForm) => new()
    {
        ImageUri = registrationForm.ImageUri ?? "https://aspnetassignment.blob.core.windows.net/images/1d0e95a8-e947-4877-8857-c15de4e55a87.svg",
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
        ImageUri = memberEntity.ImageUri,
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
        if (!string.IsNullOrWhiteSpace(updateForm.ImageUri))
            currentEntity.ImageUri = updateForm.ImageUri;
    }
}