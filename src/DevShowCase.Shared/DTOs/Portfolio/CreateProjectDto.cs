using System.ComponentModel.DataAnnotations;
namespace DevShowcase.Shared.DTOs.Portfolio;

public class CreateProjectDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Technologies { get; set; } // T.ex. "C#, React, Azure"

    [Url]
    public string? GithubUrl { get; set; }

    [Url]
    public string? ProjectUrl { get; set; }

    public string? ImageUrl { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int DisplayOrder { get; set; }
    public string ContentLanguage { get; set; } = "en";
}