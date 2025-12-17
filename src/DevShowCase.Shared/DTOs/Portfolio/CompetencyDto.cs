using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateCompetencyDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "Domain Knowledge";
    public string? Description { get; set; }
    public string ContentLanguage { get; set; } = "en";
}

public class CompetencyDto : CreateCompetencyDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}
