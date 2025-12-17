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
public class TechStackController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TechStackController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TechStackDto>>> GetTechStacks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var techStacks = await _context.TechStacks
            .Where(t => t.UserId == userId)
            // .OrderBy(t => t.Category)
            .ToListAsync();

        return Ok(techStacks.Select(t => new TechStackDto
        {
            Id = t.Id,
            UserId = t.UserId,
            Name = t.Name,
            Category = t.Category,
            IconUrl = t.IconUrl,
            ContentLanguage = t.ContentLanguage
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TechStackDto>> GetTechStack(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var techStack = await _context.TechStacks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (techStack == null) return NotFound();

        return new TechStackDto
        {
            Id = techStack.Id,
            UserId = techStack.UserId,
            Name = techStack.Name,
            Category = techStack.Category,
            IconUrl = techStack.IconUrl,
            ContentLanguage = techStack.ContentLanguage
        };
    }

    [HttpPost]
    public async Task<ActionResult<TechStackDto>> CreateTechStack(CreateTechStackDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var techStack = new TechStack
        {
            UserId = userId,
            Name = dto.Name,
            Category = dto.Category,
            IconUrl = dto.IconUrl,
            ContentLanguage = dto.ContentLanguage
        };

        _context.TechStacks.Add(techStack);
        await _context.SaveChangesAsync();

        var result = new TechStackDto
        {
            Id = techStack.Id,
            UserId = techStack.UserId,
            Name = techStack.Name,
            Category = techStack.Category,
            IconUrl = techStack.IconUrl,
            ContentLanguage = techStack.ContentLanguage
        };

        return CreatedAtAction(nameof(GetTechStack), new { id = techStack.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTechStack(int id, CreateTechStackDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var techStack = await _context.TechStacks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (techStack == null) return NotFound();

        techStack.Name = dto.Name;
        techStack.Category = dto.Category;
        techStack.IconUrl = dto.IconUrl;
        techStack.ContentLanguage = dto.ContentLanguage;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTechStack(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var techStack = await _context.TechStacks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (techStack == null) return NotFound();

        _context.TechStacks.Remove(techStack);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
