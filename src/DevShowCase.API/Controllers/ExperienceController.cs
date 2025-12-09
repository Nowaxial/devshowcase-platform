using DevShowCase.API.Data;
using DevShowCase.API.Models;
using DevShowcase.Shared.DTOs.Portfolio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace DevShowCase.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExperienceController(ApplicationDbContext context) : ControllerBase
{
    // GET: api/experience
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperienceDto>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var experiences = await context.Experiences
            .Where(e => e.UserId == userId) // VIKTIGT: Bara mina egna!
            .Select(e => new ExperienceDto
            {
                Id = e.Id,
                Company = e.Company,
                Position = e.Position,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description,
                Location = e.Location,
                IsCurrent = e.IsCurrent,
                ContentLanguage = e.ContentLanguage,
                UserId = e.UserId
            })
            .ToListAsync();
        return Ok(experiences);
    }
    // GET: api/experience/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ExperienceDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var experience = await context.Experiences
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId); // Samma här!
        if (experience == null) return NotFound();
        return Ok(new ExperienceDto
        {
            Id = experience.Id,
            Company = experience.Company, // Man kan använda AutoMapper här senare för att slippa upprepa detta
            Position = experience.Position,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description,
            Location = experience.Location,
            IsCurrent = experience.IsCurrent,
            ContentLanguage = experience.ContentLanguage,
            UserId = experience.UserId
        });
    }
    // POST: api/experience
    [HttpPost]
    public async Task<ActionResult<ExperienceDto>> Create(CreateExperienceDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // Manuell mapping från DTO -> Entity
        var experience = new Experience
        {
            UserId = userId!,
            Company = dto.Company,
            Position = dto.Position,
            Description = dto.Description,
            Location = dto.Location,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsCurrent = dto.IsCurrent,
            ContentLanguage = dto.ContentLanguage
        };
        context.Experiences.Add(experience);
        await context.SaveChangesAsync();
        // Returnera objektet med sitt nya ID
        return CreatedAtAction(nameof(GetById), new { id = experience.Id }, new ExperienceDto
        {
            Id = experience.Id,
            Company = experience.Company,
            Position = experience.Position,
            Description = experience.Description,
            Location = experience.Location,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            IsCurrent = experience.IsCurrent,
            ContentLanguage = experience.ContentLanguage,
            UserId = experience.UserId
        });
    }
    // PUT: api/experience/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateExperienceDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var experience = await context.Experiences
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (experience == null) return NotFound();
        // Uppdatera fälten
        experience.Company = dto.Company;
        experience.Position = dto.Position;
        experience.Description = dto.Description;
        experience.Location = dto.Location;
        experience.StartDate = dto.StartDate;
        experience.EndDate = dto.EndDate;
        experience.IsCurrent = dto.IsCurrent;
        experience.ContentLanguage = dto.ContentLanguage;
        await context.SaveChangesAsync();
        return NoContent();
    }
    // DELETE: api/experience/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var experience = await context.Experiences
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (experience == null) return NotFound();
        context.Experiences.Remove(experience);
        await context.SaveChangesAsync();
        return NoContent();
    }
}