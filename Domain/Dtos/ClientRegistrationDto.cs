﻿namespace Domain.Dtos;

public class ClientRegistrationDto
{
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}