namespace DevShowCase.API.Models;

public class UserAISettings
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Provider { get; set; } = "OpenAI"; // OpenAI, Anthropic, Google, OpenRouter
    public string EncryptedApiKey { get; set; } = string.Empty; // AES-256 encrypted
    public string? PreferredModel { get; set; } // e.g., "gpt-4o-mini", "claude-3-haiku"

    // Navigation
    public virtual User User { get; set; } = null!;
}
