using DevShowcase.Shared.DTOs.Portfolio;

public class ExperienceDto : CreateExperienceDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}