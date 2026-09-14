using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> users, IConfiguration configuration)
    {
        _users = users;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        if (await _users.FindByEmailAsync(email) is not null)
            return Conflict(new { message = "Já existe uma conta com este email." });

        var user = new ApplicationUser { UserName = email, Email = email, DisplayName = request.DisplayName.Trim() };
        var result = await _users.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(new { message = "Não foi possível criar a conta.", errors = result.Errors.Select(e => e.Description) });

        return Ok(CreateResponse(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _users.FindByEmailAsync(request.Email.Trim().ToLowerInvariant());
        if (user is null || !await _users.CheckPasswordAsync(user, request.Password))
            return Unauthorized(new { message = "Email ou palavra-passe incorretos." });

        return Ok(CreateResponse(user));
    }

    private AuthResponse CreateResponse(ApplicationUser user)
    {
        var expiresAt = DateTime.UtcNow.AddHours(2);
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key não está configurada.");
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "FinancialOverview",
            audience: _configuration["Jwt:Audience"] ?? "FinancialOverview.Web",
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("name", user.DisplayName)
            ],
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponse(new JwtSecurityTokenHandler().WriteToken(token), expiresAt,
            new UserResponse(user.Id, user.DisplayName, user.Email!));
    }
}

public sealed record RegisterRequest(
    [Required, StringLength(80, MinimumLength = 2)] string DisplayName,
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password);
public sealed record LoginRequest([Required, EmailAddress] string Email, [Required] string Password);
public sealed record AuthResponse(string Token, DateTime ExpiresAt, UserResponse User);
public sealed record UserResponse(string Id, string DisplayName, string Email);
