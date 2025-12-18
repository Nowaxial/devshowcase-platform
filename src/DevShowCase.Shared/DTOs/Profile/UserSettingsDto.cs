using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Profile;

public class UserAISettingsDto
{
    public bool EnableAIRewriting { get; set; } = true;
    public string? PreferredTone { get; set; } // Professional, Creative, Minimalist
    public string? CustomInstructions { get; set; }
}

public class UserPreferenceDto
{
    public bool ShowEmailPublicly { get; set; }
    public bool ShowResumeDownload { get; set; } = true;
    public string? GoogleAnalyticsId { get; set; }
}
