namespace DevShowCase.API.Models;

public class Language
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string ProficiencyLevel { get; set; } = "Intermediate"; // Native, Fluent, Professional, Intermediate, Basic
    public int DisplayOrder { get; set; } = 0;

    // Navigation
    public virtual User User { get; set; } = null!;
}
