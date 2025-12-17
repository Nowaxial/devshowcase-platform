using System;
using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateLicenseDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string IssuingOrganization { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
    public string ContentLanguage { get; set; } = "en";
}

public class LicenseDto : CreateLicenseDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}
