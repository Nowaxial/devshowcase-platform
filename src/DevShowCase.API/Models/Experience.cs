namespace DevShowCase.API.Models;

public class Experience
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; } = false;

    // Multi-language support
    public string ContentLanguage { get; set; } = "en"; // ISO 639-1

    // Navigation
    public virtual User User { get; set; } = null!;
}
