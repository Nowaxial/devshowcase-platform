using DevShowCase.API.Data;
using DevShowcase.Shared.DTOs.Portfolio;
using DevShowcase.Shared.DTOs.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController(ApplicationDbContext context) : ControllerBase
{
    // GET: api/portfolio/{username}
    [HttpGet("{username}")]
    public async Task<ActionResult<PortfolioDto>> GetPortfolio(string username)
    {
        // Try finding by UserName (email) or normalized username
        var user = await context.Users
            .Include(u => u.Experiences)
            .Include(u => u.Projects)
            .FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);

        if (user == null)
        {
            // Fallback: If "default" is requested, return first user
            if (username.ToLower() == "default")
            {
                user = await context.Users
                    .Include(u => u.Experiences)
                    .Include(u => u.Projects)
                    .FirstOrDefaultAsync();
            }

            if (user == null) return NotFound();
        }

        var portfolio = new PortfolioDto
        {
            FirstName = user.FirstName ?? "",
            LastName = user.LastName ?? "",
            Bio = user.Bio ?? "",
            Location = user.Location ?? "",
            ProfileImageUrl = user.ProfileImageUrl ?? "",
            Email = user.Email ?? "",
            
            Experiences = user.Experiences.Select(e => new ExperienceDto
            {
                Id = e.Id,
                Company = e.Company,
                Position = e.Position,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description,
                Location = e.Location,
                IsCurrent = e.IsCurrent,
                UserId = e.UserId
            }).OrderByDescending(e => e.IsCurrent).ThenByDescending(e => e.StartDate).ToList(),

            Projects = user.Projects.Select(p => new ProjectDto
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
                UserId = p.UserId
            }).OrderBy(p => p.DisplayOrder).ToList()
        };

        return Ok(portfolio);
    }
}
