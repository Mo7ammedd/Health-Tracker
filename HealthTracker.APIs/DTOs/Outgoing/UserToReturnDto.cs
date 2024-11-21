namespace HealthTracker.APIs.DTOs.Outgoing;

public class UserToReturnDto
{
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
}