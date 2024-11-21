using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities;

public class RefreshToken : BaseEntity
{
    
    public string UserId { get; set; } //user id who owns this token
    
    public string Token { get; set; }
    
    public string JwtId { get; set; } //unique identifier for this token
    
    public bool IsUsed { get; set; } // to make sure token is used only once
    
    public bool IsRevoked { get; set; }
    
    public DateTime Expires { get; set; } 
    
    [ForeignKey(nameof(UserId))]
    
    public User User { get; set; }
}