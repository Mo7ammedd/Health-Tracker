using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HealthTracker.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IConfiguration config, IUnitOfWork unitOfWork)
    {
        _config = config;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> CreateTokenAsync(User user, UserManager<User> userManager)
    {
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
        };
        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
        var token = new JwtSecurityToken(
            issuer: _config["JWT:ValidIssuer"],
            audience: _config["JWT:ValidAudience"],
            expires: DateTime.UtcNow.AddDays(double.Parse(_config["JWT:TokenLifeTime"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> RefreshTokenAsync(RefreshToken token, User user, UserManager<User> userManager)
    {
        var storedRefreshToken = await _unitOfWork.Repository<RefreshToken>().GetById(token.Id);
        if (storedRefreshToken == null || storedRefreshToken.IsRevoked || storedRefreshToken.IsUsed)
        {
            return null;
        }

        if (storedRefreshToken.Expires < DateTime.UtcNow)
        {
            return null;
        }
        storedRefreshToken.IsUsed = true;
        await _unitOfWork.Repository<RefreshToken>().Update(storedRefreshToken);

        return await CreateTokenAsync(user, userManager);
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(User user)
    {
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            JwtId = Guid.NewGuid().ToString(),
            IsUsed = false,
            IsRevoked = false,
            Expires = DateTime.UtcNow.AddDays(7), 
            User = user
        };

        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
        return refreshToken;
    }

    public async Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken)
    {
        var token = await _unitOfWork.Repository<RefreshToken>().FirstOrDefaultAsync(t => t.Token == refreshToken && t.UserId == user.Id);
        return token != null && !token.IsUsed && !token.IsRevoked && token.Expires > DateTime.UtcNow;
    }
}