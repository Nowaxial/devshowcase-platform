using System;
using System.ComponentModel.DataAnnotations;

namespace DevShowcase.Shared.DTOs.Public;

public class ContactMessageDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Message { get; set; } = string.Empty;
    
    public DateTime SentDate { get; set; }
}

public class AdminContactMessageDto : ContactMessageDto
{
    public int Id { get; set; }
    public bool IsRead { get; set; }
}
