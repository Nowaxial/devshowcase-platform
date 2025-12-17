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
public class EducationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EducationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EducationDto>>> GetEducation()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var educations = await _context.Education
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();

        var dtos = educations.Select(e => new EducationDto
        {
            Id = e.Id,
            UserId = e.UserId,
            Institution = e.Institution,
            Degree = e.Degree,
            Field = e.Field,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            IsCurrent = e.IsCurrent,
            Location = e.Location,
            ContentLanguage = e.ContentLanguage
        }).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EducationDto>> GetEducation(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var education = await _context.Education
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (education == null) return NotFound();

        return new EducationDto
        {
            Id = education.Id,
            UserId = education.UserId,
            Institution = education.Institution,
            Degree = education.Degree,
            Field = education.Field,
            Description = education.Description,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            IsCurrent = education.IsCurrent,
            Location = education.Location,
            ContentLanguage = education.ContentLanguage
        };
    }

    [HttpPost]
    public async Task<ActionResult<EducationDto>> CreateEducation(CreateEducationDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var education = new Education
        {
            UserId = userId,
            Institution = dto.Institution,
            Degree = dto.Degree,
            Field = dto.Field,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsCurrent = dto.IsCurrent,
            Location = dto.Location ?? string.Empty,
            ContentLanguage = dto.ContentLanguage
        };

        _context.Education.Add(education);
        await _context.SaveChangesAsync();

        var resultDto = new EducationDto
        {
            Id = education.Id,
            UserId = education.UserId,
            Institution = education.Institution,
            Degree = education.Degree,
            Field = education.Field,
            Description = education.Description,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            IsCurrent = education.IsCurrent,
            Location = education.Location,
            ContentLanguage = education.ContentLanguage
        };

        return CreatedAtAction(nameof(GetEducation), new { id = education.Id }, resultDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEducation(int id, CreateEducationDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var education = await _context.Education
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (education == null) return NotFound();

        education.Institution = dto.Institution;
        education.Degree = dto.Degree;
        education.Field = dto.Field;
        education.Description = dto.Description;
        education.StartDate = dto.StartDate;
        education.EndDate = dto.EndDate;
        education.IsCurrent = dto.IsCurrent;
        education.Location = dto.Location ?? string.Empty;
        education.ContentLanguage = dto.ContentLanguage;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var education = await _context.Education
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (education == null) return NotFound();

        _context.Education.Remove(education);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
