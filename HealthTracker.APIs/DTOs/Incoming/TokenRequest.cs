using System.ComponentModel.DataAnnotations;

namespace HealthTracker.APIs.DTOs;

public class TokenRequest
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToekn { get; set; }
}