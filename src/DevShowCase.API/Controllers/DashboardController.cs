using System.Security.Claims;
using DevShowCase.API.Data;
using DevShowcase.Shared.DTOs.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetStats()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var stats = new DashboardStatsDto
        {
            TotalProjects = await _context.Projects.CountAsync(x => x.UserId == userId),
            TotalExperience = await _context.Experiences.CountAsync(x => x.UserId == userId),
            TotalEducation = await _context.Education.CountAsync(x => x.UserId == userId),
            TotalSkills = await _context.Skills.CountAsync(x => x.UserId == userId),
            TotalLicenses = await _context.Licenses.CountAsync(x => x.UserId == userId),
            TotalLanguages = await _context.Languages.CountAsync(x => x.UserId == userId),
            TotalTechStack = await _context.TechStacks.CountAsync(x => x.UserId == userId),
            UnreadMessagesCount = await _context.ContactMessages.CountAsync(x => x.UserId == userId && !x.IsRead)
        };

        return Ok(stats);
    }
}
