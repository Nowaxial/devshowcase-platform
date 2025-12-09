using DevShowcase.API.Services;
using DevShowcase.Shared.DTOs.Auth;
using DevShowCase.API.Models;
using DevShowCase.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevShowCase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        // 1. Kolla om användaren redan finns
        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return BadRequest("User with this email already exists");
        // 2. Skapa användarobjektet
        var user = new User
        {
            UserName = registerDto.Email, // Identity använder UserName för inloggning
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            ProfileImageUrl = "https://placehold.co/150", // Placeholder tills vidare
            SelectedThemeId = null // Inget tema valt
        };
        // 3. Spara till databas (lösenordet hashas automatiskt här!)
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            // Om lösenordet inte uppfyller kraven (t.ex. för kort) får vi fel här
            return BadRequest(result.Errors);
        }

        // 4. (Optional) Lägg till roll om vi hade roller
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
        // Nu kan vi säkert lägga till den
        await userManager.AddToRoleAsync(user, "User");
        return Ok(new { message = "User registered successfully!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // 1. Hitta användaren
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Unauthorized("Invalid email or password");
        // 2. Kontrollera lösenordet
        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
            return Unauthorized("Invalid email or password");
        // 3. Hämta roller (behövs för token)
        var roles = await userManager.GetRolesAsync(user);
        // 4. SKAPA TOKEN (Här använder vi äntligen tokenService!)
        var accessToken = tokenService.GenerateAccessToken(user, roles);
        // 5. Returnera token
        return Ok(new { token = accessToken });
    }
}
