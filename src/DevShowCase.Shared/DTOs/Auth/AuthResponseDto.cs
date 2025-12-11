namespace DevShowcase.Shared.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
    public List<string> Errors { get; set; } = new();
}
