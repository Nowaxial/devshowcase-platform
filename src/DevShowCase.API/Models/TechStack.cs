namespace DevShowCase.API.Models;

public class TechStack
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; } // e.g., "Frontend", "Backend", "Database", "DevOps"
    public string? IconUrl { get; set; } // URL to technology icon
    public int DisplayOrder { get; set; } = 0;

    // Multi-language support
    public string ContentLanguage { get; set; } = "en";

    // Navigation
    public virtual User User { get; set; } = null!;
}