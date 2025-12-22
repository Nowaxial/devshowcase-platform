using Microsoft.AspNetCore.Identity;

namespace DevShowCase.API.Models;

public class User : IdentityUser
{
    // Basic Info
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? AboutMe { get; set; }
    public string? Location { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? LinkedInUrl { get; set; }

    // Preferences
    public int? SelectedThemeId { get; set; }
    public bool PrefersDarkMode { get; set; } = false;
    public string PreferredLanguage { get; set; } = "en";

    // Navigation Properties
    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Education> Education { get; set; } = new List<Education>();
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
    public virtual ICollection<Competency> Competencies { get; set; } = new List<Competency>();
    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();
    public virtual ICollection<TechStack> TechStacks { get; set; } = new List<TechStack>();
    public virtual ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();

    // Relationships
    public virtual Theme? SelectedTheme { get; set; }
    public virtual UserAISettings? AISettings { get; set; }
}
