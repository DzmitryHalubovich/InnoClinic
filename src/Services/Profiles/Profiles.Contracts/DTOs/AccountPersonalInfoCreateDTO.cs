﻿using System.ComponentModel.DataAnnotations;

namespace Profiles.Contracts.DTOs;

public class AccountPersonalInfoCreateDTO
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(100)]
    public string? MiddleName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
}
