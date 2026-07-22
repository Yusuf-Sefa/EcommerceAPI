

using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthUserDto request)
    {
        var user = await _authService.RegisterAsync(request);

        return user is null
                ? BadRequest("Email already exists")
                : Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto request)
    {
        var result = await _authService.LoginAsync(request);

        return result is null
                ? Unauthorized( new { message = "Invalid username or password"})
                : Ok(result);
    }

    [Authorize]
    [HttpGet("authenticatedOnly")]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are authenticated!");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("adminOnly")]
    public IActionResult AdminOnlyEndpoint()
    {
        return Ok("You are and admin!");
    }

}