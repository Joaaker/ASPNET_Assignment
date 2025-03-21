﻿using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class SignUpFormModel
{
    [Display(Name = "Full Name", Prompt = "Your full name")]
    [Required(ErrorMessage = "Required")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Invalid")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Compare(nameof(Password), ErrorMessage = "Invalid")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    public bool TermsAndConditions { get; set; }
}