using HealthTracker.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthTracker.Core.Services.Contract;

public interface IAuthService
{
    Task<string> CreateTokenAsync(User user, UserManager<User> userManager);
    Task<string> RefreshTokenAsync(RefreshToken token, User user, UserManager<User> userManager);
    Task<RefreshToken> GenerateRefreshTokenAsync(User user);
    Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken);
}