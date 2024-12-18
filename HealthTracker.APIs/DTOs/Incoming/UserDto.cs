﻿using System.ComponentModel.DataAnnotations;

namespace HealthTracker.APIs.DTOs;

public class UserDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [RegularExpression( "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$", ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non alphanumeric and at least 8 characters")]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}