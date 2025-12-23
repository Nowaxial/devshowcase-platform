using System.Security.Claims;
using DevShowCase.API.Data;
using DevShowCase.API.Models;
using DevShowcase.Shared.DTOs.Profile;
using DevShowcase.Shared.DTOs.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public ProfileController(UserManager<User> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<UpdateProfileDto>> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        // Used PortfolioDto here as it contains a good summary of user data,
        // but typically you might want a specific 'UserProfileDto' for editing
        var user = await _context.Users
            .Include(u => u.SelectedTheme)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound();

        // Mapping to UpdateProfileDto for the "Edit" form, or return a full structure.
        // Let's typically return the same structure as we Update
        return Ok(new UpdateProfileDto
        {
             FirstName = user.FirstName,
             LastName = user.LastName,
             Bio = user.Bio,
             AboutMe = user.AboutMe,
             Location = user.Location,
             ProfileImageUrl = user.ProfileImageUrl,
             GithubUrl = user.GithubUrl,
             LinkedInUrl = user.LinkedInUrl,
             SelectedThemeId = user.SelectedThemeId,
             PrefersDarkMode = user.PrefersDarkMode,
             PreferredLanguage = user.PreferredLanguage
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Bio = dto.Bio;
        user.AboutMe = dto.AboutMe;
        user.Location = dto.Location;
        user.ProfileImageUrl = dto.ProfileImageUrl;
        user.GithubUrl = dto.GithubUrl;
        user.LinkedInUrl = dto.LinkedInUrl;
        user.SelectedThemeId = dto.SelectedThemeId;
        user.PrefersDarkMode = dto.PrefersDarkMode;
        user.PreferredLanguage = dto.PreferredLanguage;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}
