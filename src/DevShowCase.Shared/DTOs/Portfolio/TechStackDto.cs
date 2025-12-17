using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateTechStackDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "Tool";
    public string? IconUrl { get; set; }
    public decimal? YearsOfExperience { get; set; }
    public string ContentLanguage { get; set; } = "en";
}

public class TechStackDto : CreateTechStackDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}
