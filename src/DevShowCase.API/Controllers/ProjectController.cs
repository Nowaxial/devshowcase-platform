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
public class ProjectController(ApplicationDbContext context) : ControllerBase
{
    // GET: api/project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var projects = await context.Projects
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.DisplayOrder) // Sortera ordningen!
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Technologies = p.Technologies,
                GithubUrl = p.GithubUrl,
                ProjectUrl = p.LiveUrl,
                ImageUrl = p.ImageUrl,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                DisplayOrder = p.DisplayOrder,
                ContentLanguage = p.ContentLanguage,
                UserId = p.UserId
            })
            .ToListAsync();
        return Ok(projects);
    }
    // GET: api/project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = await context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();
        return Ok(new ProjectDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Technologies = project.Technologies,
            GithubUrl = project.GithubUrl,
            ProjectUrl = project.LiveUrl,
            ImageUrl = project.ImageUrl,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            DisplayOrder = project.DisplayOrder,
            ContentLanguage = project.ContentLanguage,
            UserId = project.UserId
        });
    }
    // POST: api/project
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = new Project
        {
            UserId = userId!,
            Title = dto.Title,
            Description = dto.Description,
            Technologies = dto.Technologies,
            GithubUrl = dto.GithubUrl,
            LiveUrl = dto.ProjectUrl,
            ImageUrl = dto.ImageUrl,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            DisplayOrder = dto.DisplayOrder,
            ContentLanguage = dto.ContentLanguage
        };
        context.Projects.Add(project);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, new ProjectDto
        {
            Id = project.Id,
            UserId = project.UserId,
            Title = project.Title,
            Description = project.Description,
            Technologies = project.Technologies,
            GithubUrl = project.GithubUrl,
            ProjectUrl = project.LiveUrl,
            ImageUrl = project.ImageUrl,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            DisplayOrder = project.DisplayOrder,
            ContentLanguage = project.ContentLanguage
        });
    }
    // PUT: api/project/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateProjectDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = await context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();
        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Technologies = dto.Technologies;
        project.GithubUrl = dto.GithubUrl;
        project.LiveUrl = dto.ProjectUrl;
        project.ImageUrl = dto.ImageUrl;
        project.StartDate = dto.StartDate;
        project.EndDate = dto.EndDate;
        project.DisplayOrder = dto.DisplayOrder;
        project.ContentLanguage = dto.ContentLanguage;
        await context.SaveChangesAsync();
        return NoContent();
    }
    // DELETE: api/project/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = await context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();
        context.Projects.Remove(project);
        await context.SaveChangesAsync();
        return NoContent();
    }
}