using System.Security.Claims;
using DevShowCase.API.Data;
using DevShowCase.API.Models;
using DevShowcase.Shared.DTOs.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SettingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SettingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("ai")]
    public async Task<ActionResult<UserAISettingsDto>> GetAISettings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var settings = await _context.UserAISettings.FirstOrDefaultAsync(s => s.UserId == userId);
        
        if (settings == null) 
            return Ok(new UserAISettingsDto());

        return Ok(new UserAISettingsDto
        {
            EnableAIRewriting = settings.EnableAIRewriting,
            PreferredTone = settings.PreferredTone,
            CustomInstructions = settings.CustomInstructions
        });
    }

    [HttpPut("ai")]
    public async Task<IActionResult> UpdateAISettings(UserAISettingsDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var settings = await _context.UserAISettings.FirstOrDefaultAsync(s => s.UserId == userId);

        if (settings == null)
        {
            settings = new UserAISettings { UserId = userId! };
            _context.UserAISettings.Add(settings);
        }

        settings.EnableAIRewriting = dto.EnableAIRewriting;
        settings.PreferredTone = dto.PreferredTone;
        settings.CustomInstructions = dto.CustomInstructions;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("preferences")]
    public async Task<ActionResult<UserPreferenceDto>> GetPreferences()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var prefs = await _context.UserPreferences.FirstOrDefaultAsync(p => p.UserId == userId);

        if (prefs == null)
            return Ok(new UserPreferenceDto());

        return Ok(new UserPreferenceDto
        {
            ShowEmailPublicly = prefs.ShowEmailPublicly,
            ShowResumeDownload = prefs.ShowResumeDownload,
            GoogleAnalyticsId = prefs.GoogleAnalyticsId
        });
    }

    [HttpPut("preferences")]
    public async Task<IActionResult> UpdatePreferences(UserPreferenceDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var prefs = await _context.UserPreferences.FirstOrDefaultAsync(p => p.UserId == userId);

        if (prefs == null)
        {
            prefs = new UserPreference { UserId = userId! };
            _context.UserPreferences.Add(prefs);
        }

        prefs.ShowEmailPublicly = dto.ShowEmailPublicly;
        prefs.ShowResumeDownload = dto.ShowResumeDownload;
        prefs.GoogleAnalyticsId = dto.GoogleAnalyticsId;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
