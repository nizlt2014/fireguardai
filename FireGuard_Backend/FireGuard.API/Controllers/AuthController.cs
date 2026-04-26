using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FireGuard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace FireGuard.API.Controllers;
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly IConfiguration _config;

    public AuthController(IUserRepository users, IConfiguration config)
    {
        _users = users;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _users.GetByEmailAsync(request.Email);

        if (user == null)
            return Unauthorized("Invalid credentials");

        bool valid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!valid)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwt(user);

        return Ok(new { token });
    }

    private string GenerateJwt(dynamic user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}