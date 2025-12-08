namespace DevShowCase.API.Models;

public class Project
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Technologies { get; set; }
    public string? GithubUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int DisplayOrder { get; set; } = 0;

    // Multi-language support
    public string ContentLanguage { get; set; } = "en";

    // Navigation
    public virtual User User { get; set; } = null!;
}
