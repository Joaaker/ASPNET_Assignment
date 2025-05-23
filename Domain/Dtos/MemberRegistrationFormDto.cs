﻿namespace Domain.Dtos;

public class MemberRegistrationFormDto
{
    public string RoleName { get; set; } = null!;
    public string? ImageUri { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? JobTitle { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? StreetName { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}