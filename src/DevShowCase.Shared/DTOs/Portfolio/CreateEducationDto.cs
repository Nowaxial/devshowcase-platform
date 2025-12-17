using System;
using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateEducationDto
{
    [Required]
    public string Institution { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string? Field { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string? Location { get; set; }
    public string ContentLanguage { get; set; } = "en";
}
