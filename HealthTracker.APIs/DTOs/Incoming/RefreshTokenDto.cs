using System.ComponentModel.DataAnnotations;

namespace HealthTracker.APIs.DTOs;

public class RefreshTokenDto
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}