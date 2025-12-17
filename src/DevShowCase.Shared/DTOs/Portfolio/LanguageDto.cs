using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateLanguageDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string ProficiencyLevel { get; set; } = "Intermediate"; // Native, Fluent, Professional, Intermediate, Basic
    public string ContentLanguage { get; set; } = "en";
}

public class LanguageDto : CreateLanguageDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}
