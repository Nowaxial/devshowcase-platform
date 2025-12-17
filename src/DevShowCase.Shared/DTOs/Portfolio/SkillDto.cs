namespace DevShowcase.Shared.DTOs.Portfolio;

public class SkillDto : CreateSkillDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}
