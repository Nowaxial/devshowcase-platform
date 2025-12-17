using System.Security.Claims;
using DevShowCase.API.Data;
using DevShowCase.API.Models;
using DevShowcase.Shared.DTOs.Portfolio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LanguageController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LanguageController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LanguageDto>>> GetLanguages()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var languages = await _context.Languages
            .Where(l => l.UserId == userId)
            .OrderBy(l => l.DisplayOrder)
            .ToListAsync();

        return Ok(languages.Select(l => new LanguageDto
        {
            Id = l.Id,
            UserId = l.UserId,
            Name = l.Name,
            ProficiencyLevel = l.ProficiencyLevel,
            
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LanguageDto>> GetLanguage(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var language = await _context.Languages
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        if (language == null) return NotFound();

        return new LanguageDto
        {
            Id = language.Id,
            UserId = language.UserId,
            Name = language.Name,
            ProficiencyLevel = language.ProficiencyLevel,
            
        };
    }

    [HttpPost]
    public async Task<ActionResult<LanguageDto>> CreateLanguage(CreateLanguageDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var language = new Language
        {
            UserId = userId,
            Name = dto.Name,
            ProficiencyLevel = dto.ProficiencyLevel,
            
        };

        _context.Languages.Add(language);
        await _context.SaveChangesAsync();

        var result = new LanguageDto
        {
            Id = language.Id,
            UserId = language.UserId,
            Name = language.Name,
            ProficiencyLevel = language.ProficiencyLevel,
            
        };

        return CreatedAtAction(nameof(GetLanguage), new { id = language.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLanguage(int id, CreateLanguageDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var language = await _context.Languages
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        if (language == null) return NotFound();

        language.Name = dto.Name;
        language.ProficiencyLevel = dto.ProficiencyLevel;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var language = await _context.Languages
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        if (language == null) return NotFound();

        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
