namespace DevShowcase.Shared.DTOs.Portfolio;

public class ProjectDto : CreateProjectDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}