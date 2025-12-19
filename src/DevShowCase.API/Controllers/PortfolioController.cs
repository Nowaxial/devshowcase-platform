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
            .Include(u => u.Education)
            .Include(u => u.Skills)
            .Include(u => u.Languages)
            .Include(u => u.TechStacks)
            .Include(u => u.Competencies)
            .Include(u => u.Licenses)
            .Include(u => u.SelectedTheme)
            .FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);

        if (user == null)
        {
            // Fallback: If "default" is requested, return first user
            if (username.ToLower() == "default")
            {
                user = await context.Users
                    .Include(u => u.Experiences)
                    .Include(u => u.Projects)
                    .Include(u => u.Education)
                    .Include(u => u.Skills)
                    .Include(u => u.Languages)
                    .Include(u => u.TechStacks)
                    .Include(u => u.Competencies)
                    .Include(u => u.Licenses)
                    .Include(u => u.SelectedTheme)
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
            ThemeCssFile = user.SelectedTheme?.CssFileName ?? "theme-minimal.css",
            
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
            }).OrderBy(p => p.DisplayOrder).ToList(),

            Education = user.Education.Select(e => new EducationDto
            {
                Id = e.Id,
                Institution = e.Institution,
                Degree = e.Degree,
                Field = e.Field,
                Location = e.Location,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description
            }).OrderByDescending(e => e.StartDate).ToList(),

            Skills = user.Skills.Select(s => new SkillDto
            {
                Id = s.Id,
                Name = s.Name,
                Category = s.Category
            }).OrderBy(s => s.Category).ToList(),

            Languages = user.Languages.Select(l => new LanguageDto
            {
                Id = l.Id,
                Name = l.Name,
                ProficiencyLevel = l.ProficiencyLevel
            }).ToList(),

            TechStacks = user.TechStacks.Select(t => new TechStackDto
            {
                Id = t.Id,
                Name = t.Name,
                IconUrl = t.IconUrl,
                Category = t.Category ?? "Other"
            }).OrderBy(t => t.Category).ToList(),

            Competencies = user.Competencies.Select(c => new CompetencyDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Category = c.Category
            }).OrderBy(c => c.Category).ToList(),

            Licenses = user.Licenses.Select(l => new LicenseDto
            {
                Id = l.Id,
                Name = l.Name,
                IssuingOrganization = l.Issuer ?? "",
                IssueDate = l.IssueDate ?? DateTime.MinValue,
                ExpirationDate = l.ExpiryDate,
                CredentialId = l.CredentialId,
                CredentialUrl = l.CredentialUrl
            }).OrderByDescending(l => l.IssueDate).ToList()
        };

        return Ok(portfolio);
    }
}
