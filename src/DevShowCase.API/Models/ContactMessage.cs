namespace DevShowCase.API.Models;

public class ContactMessage
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty; // Portfolio owner

    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;

    // Navigation
    public virtual User User { get; set; } = null!;
}

