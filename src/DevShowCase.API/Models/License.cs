namespace DevShowCase.API.Models;

public class License
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string? Issuer { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }

    // Multi-language support
    public string ContentLanguage { get; set; } = "en";

    // Navigation
    public virtual User User { get; set; } = null!;
}
