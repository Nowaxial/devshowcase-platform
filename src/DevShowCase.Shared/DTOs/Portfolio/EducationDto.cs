using System;

namespace DevShowcase.Shared.DTOs.Portfolio;

public class EducationDto : CreateEducationDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}
