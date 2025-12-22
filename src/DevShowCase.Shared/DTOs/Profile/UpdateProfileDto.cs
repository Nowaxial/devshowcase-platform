namespace DevShowcase.Shared.DTOs.Profile;

public class UpdateProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? Location { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public int? SelectedThemeId { get; set; }
    public bool PrefersDarkMode { get; set; }
    public string PreferredLanguage { get; set; } = "en";
}
