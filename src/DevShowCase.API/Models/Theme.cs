namespace DevShowCase.API.Models;

public class Theme
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CssFileName { get; set; } = string.Empty; // e.g., "selfso-minimal.css"
    public bool IsActive { get; set; } = true;

    public string? PreviewImageUrl { get; set; }
    public bool SupportsDarkMode { get; set; } = true;
    public string TargetAudience { get; set; } = "General"; // Frontend, Backend, etc.

    // Navigation
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
