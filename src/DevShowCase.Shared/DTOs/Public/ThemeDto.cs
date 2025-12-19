namespace DevShowcase.Shared.DTOs.Public;

public class ThemeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CssFileName { get; set; } = string.Empty;
    public string? PreviewImageUrl { get; set; }
    public string TargetAudience { get; set; } = string.Empty;
}
