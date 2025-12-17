using DevShowcase.Shared.DTOs.Portfolio;

namespace DevShowcase.Shared.DTOs.Public;

public class PortfolioDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;

    public List<ExperienceDto> Experiences { get; set; } = new();
    public List<ProjectDto> Projects { get; set; } = new();
    // Add Education, Skills, etc later when controllers exist
}
