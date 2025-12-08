namespace DevShowCase.API.Models;

public class UserPreference
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }

    // Navigation
    public virtual User User { get; set; } = null!;
}
