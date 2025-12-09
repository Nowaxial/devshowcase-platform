using System.ComponentModel.DataAnnotations;
namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateExperienceDto
{
    [Required]
    public string Company { get; set; } = string.Empty;
    [Required]
    public string Position { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string ContentLanguage { get; set; } = "en";
}