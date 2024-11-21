using HealthTracker.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthTracker.Core.Services.Contract;

public interface IAuthService
{
    
    Task<string> CreateTokenAsync(User user,UserManager<User> userManager);
}