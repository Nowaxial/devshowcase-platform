namespace DevShowCase.API.Models;

public class UserPreference
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public bool ShowEmailPublicly { get; set; }
    public bool ShowResumeDownload { get; set; } = true;
    public string? GoogleAnalyticsId { get; set; }

    // Navigation
    public virtual User User { get; set; } = null!;
}
