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
public class SkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SkillController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var skills = await _context.Skills
            .Where(s => s.UserId == userId)
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Name)
            .ToListAsync();

        return Ok(skills.Select(s => new SkillDto
        {
            Id = s.Id,
            UserId = s.UserId,
            Name = s.Name,
            Category = s.Category,
            DisplayOrder = s.DisplayOrder,
            ContentLanguage = s.ContentLanguage
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SkillDto>> GetSkill(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (skill == null) return NotFound();

        return new SkillDto
        {
            Id = skill.Id,
            UserId = skill.UserId,
            Name = skill.Name,
            Category = skill.Category,
            DisplayOrder = skill.DisplayOrder,
            ContentLanguage = skill.ContentLanguage
        };
    }

    [HttpPost]
    public async Task<ActionResult<SkillDto>> CreateSkill(CreateSkillDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var skill = new Skill
        {
            UserId = userId,
            Name = dto.Name,
            Category = dto.Category,
            DisplayOrder = dto.DisplayOrder,
            ContentLanguage = dto.ContentLanguage
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        var result = new SkillDto
        {
            Id = skill.Id,
            UserId = skill.UserId,
            Name = skill.Name,
            Category = skill.Category,
            DisplayOrder = skill.DisplayOrder,
            ContentLanguage = skill.ContentLanguage
        };

        return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkill(int id, CreateSkillDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (skill == null) return NotFound();

        skill.Name = dto.Name;
        skill.Category = dto.Category;
        skill.DisplayOrder = dto.DisplayOrder;
        skill.ContentLanguage = dto.ContentLanguage;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (skill == null) return NotFound();

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
