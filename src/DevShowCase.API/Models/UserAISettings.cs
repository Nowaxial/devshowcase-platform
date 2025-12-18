namespace DevShowCase.API.Models;

public class UserAISettings
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Provider { get; set; } = "OpenAI"; 
    public string EncryptedApiKey { get; set; } = string.Empty;
    public string? PreferredModel { get; set; }
    public bool EnableAIRewriting { get; set; } = true;
    public string? PreferredTone { get; set; }
    public string? CustomInstructions { get; set; }

    // Navigation
    public virtual User User { get; set; } = null!;
}
