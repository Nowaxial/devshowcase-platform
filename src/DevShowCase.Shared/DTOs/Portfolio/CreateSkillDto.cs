using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateSkillDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public int DisplayOrder { get; set; } = 0;
    public string ContentLanguage { get; set; } = "en";
}
