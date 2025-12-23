using DevShowCase.API.Services;
using DevShowcase.Shared.DTOs.Auth;
using DevShowCase.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevShowCase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new AuthResponseDto { Success = false, Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList() });

        // 1. Kolla om användaren redan finns
        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return BadRequest(new AuthResponseDto { Success = false, Errors = new List<string> { "User with this email already exists" } });

        // 2. Generera unikt användarnamn
        var baseUsername = $"{registerDto.FirstName.ToLower()}{registerDto.LastName.ToLower()}";
        var username = baseUsername;
        var counter = 1;

        // Kolla om användarnamnet redan finns
        while (await userManager.FindByNameAsync(username) != null)
        {
            username = $"{baseUsername}{counter}";
            counter++;
        }

        // 3. Skapa användarobjektet
        var user = new User
        {
            UserName = username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            ProfileImageUrl = "https://placehold.co/150",
            SelectedThemeId = 1
        };

        // 4. Spara till databas
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new AuthResponseDto { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() });
        }

        // 5. Lägg till roll
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
        await userManager.AddToRoleAsync(user, "User");

        return Ok(new AuthResponseDto { Success = true, Token = "", RefreshToken = "" });
    }


    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        // 1. Hitta användaren
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        
        if (user == null)
            return Unauthorized(new AuthResponseDto { Success = false, Errors = new List<string> { "Invalid email or password" } });

        // 2. Kontrollera lösenordet
        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
            return Unauthorized(new AuthResponseDto { Success = false, Errors = new List<string> { "Invalid email or password" } });

        // 3. Hämta roller
        var roles = await userManager.GetRolesAsync(user);

        // 4. SKAPA TOKEN
        var accessToken = tokenService.GenerateAccessToken(user, roles);
        var refreshToken = tokenService.GenerateRefreshToken();

        // 5. Returnera token och DTO
        return Ok(new AuthResponseDto 
        { 
            Success = true, 
            Token = accessToken, 
            RefreshToken = refreshToken 
        });
    }
}
