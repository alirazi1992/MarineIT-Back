using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarineIT.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MarinIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<AppUser> users, SignInManager<AppUser> signInMgr, IConfiguration cfg) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var u = new AppUser { UserName = dto.UserName, Email = dto.Email };
        var res = await users.CreateAsync(u, dto.Password);
        if (!res.Succeeded) return BadRequest(res.Errors);
        if (!string.IsNullOrWhiteSpace(dto.Role)) await users.AddToRoleAsync(u, dto.Role);
        return Ok(new { u.Id, u.UserName, dto.Role });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var u = await users.FindByNameAsync(dto.UserName);
        if (u is null) return Unauthorized();
        var ok = await signInMgr.CheckPasswordSignInAsync(u, dto.Password, false);
        if (!ok.Succeeded) return Unauthorized();

        var jwt = cfg.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await users.GetRolesAsync(u);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, u.Id),
            new(ClaimTypes.Name, u.UserName ?? "")
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me() =>
        Ok(new { name = User.Identity?.Name, roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) });
}

public record RegisterDto(string UserName, string Email, string Password, string Role);
public record LoginDto(string UserName, string Password);
