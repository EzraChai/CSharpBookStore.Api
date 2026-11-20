using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController() : ControllerBase
{
    private readonly string jwtKey =
        Environment.GetEnvironmentVariable("JWT_KEY")
        ?? throw new InvalidOperationException("JWT_KEY is missing.");
    private readonly string jwtIssuer =
        Environment.GetEnvironmentVariable("JWT_ISSUER")
        ?? throw new InvalidOperationException("JWT_ISSUER is missing.");
    private readonly string jwtAudience =
        Environment.GetEnvironmentVariable("JWT_AUDIENCE")
        ?? throw new InvalidOperationException("JWT_AUDIENCE is missing.");

    [HttpPost("login")]
    public IActionResult Login(LoginDto request)
    {
        // Demo credentials only
        if (request.Username != "admin" ||
            request.Password != "password123")
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            tokenType = "Bearer",
            expiresIn = 3600
        });
    }
}

public record LoginDto(string Username, string Password);