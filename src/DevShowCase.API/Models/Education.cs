namespace DevShowCase.API.Models;

public class Education
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string? Field { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; } = false;

    // Multi-language support
    public string ContentLanguage { get; set; } = "en";

    // Navigation
    public virtual User User { get; set; } = null!;
}