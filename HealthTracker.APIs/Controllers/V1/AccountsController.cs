using HealthTracker.APIs.DTOs;
using HealthTracker.APIs.DTOs.Outgoing;
using HealthTracker.APIs.Errors;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.APIs.Controllers.V1;

public class AccountsControllers : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthService _authService;

    public AccountsControllers(UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserToReturnDto>> Register([FromBody]RegisterDto registerDto)
    {
        if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
        {
            return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "Email address is in use" } });
        }
        var user = new User()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.Email.Split("@")[0],
            PhoneNumber = registerDto.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new ApiResponse(400));
        }

        var refreshToken = await _authService.GenerateRefreshTokenAsync(user);

        return new UserToReturnDto()
        {
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager),
            DisplayName = user.FirstName,
            RefreshToken = refreshToken.Token
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserToReturnDto>> Login([FromBody]LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var refreshToken = await _authService.GenerateRefreshTokenAsync(user);

        return new UserToReturnDto()
        {
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager),
            DisplayName = user.FirstName,
            RefreshToken = refreshToken.Token
        };
    }
    [HttpPost("refresh-token")]
    public async Task<ActionResult<UserToReturnDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var user = await _userManager.FindByEmailAsync(refreshTokenDto.Email);
        if (user == null || !await _authService.ValidateRefreshTokenAsync(user, refreshTokenDto.Token))
        {
            return Unauthorized(new ApiResponse(401, "Invalid refresh token."));
        }
    
        var newToken = await _authService.CreateTokenAsync(user, _userManager);
        var newRefreshToken = await _authService.GenerateRefreshTokenAsync(user);
    
        return new UserToReturnDto
        {
            Email = user.Email,
            Token = newToken,
            DisplayName = user.FirstName,
            RefreshToken = newRefreshToken.Token
        };
    }


    [HttpGet("email-exists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }
}