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
public class CompetencyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompetencyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompetencyDto>>> GetCompetencies()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var competencies = await _context.Competencies
            .Where(c => c.UserId == userId)
            // .OrderBy(c => c.Category) // Optional sorting
            .ToListAsync();

        return Ok(competencies.Select(c => new CompetencyDto
        {
            Id = c.Id,
            UserId = c.UserId,
            Name = c.Name,
            Category = c.Category,
            Description = c.Description,
            ContentLanguage = c.ContentLanguage
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompetencyDto>> GetCompetency(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var c = await _context.Competencies
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (c == null) return NotFound();

        return new CompetencyDto
        {
            Id = c.Id,
            UserId = c.UserId,
            Name = c.Name,
            Category = c.Category,
            Description = c.Description,
            ContentLanguage = c.ContentLanguage
        };
    }

    [HttpPost]
    public async Task<ActionResult<CompetencyDto>> CreateCompetency(CreateCompetencyDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var competency = new Competency
        {
            UserId = userId,
            Name = dto.Name,
            Category = dto.Category,
            Description = dto.Description,
            ContentLanguage = dto.ContentLanguage
        };

        _context.Competencies.Add(competency);
        await _context.SaveChangesAsync();

        var result = new CompetencyDto
        {
            Id = competency.Id,
            UserId = competency.UserId,
            Name = competency.Name,
            Category = competency.Category,
            Description = competency.Description,
            ContentLanguage = competency.ContentLanguage
        };

        return CreatedAtAction(nameof(GetCompetency), new { id = competency.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompetency(int id, CreateCompetencyDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var competency = await _context.Competencies
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (competency == null) return NotFound();

        competency.Name = dto.Name;
        competency.Category = dto.Category;
        competency.Description = dto.Description;
        competency.ContentLanguage = dto.ContentLanguage;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompetency(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var competency = await _context.Competencies
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (competency == null) return NotFound();

        _context.Competencies.Remove(competency);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
