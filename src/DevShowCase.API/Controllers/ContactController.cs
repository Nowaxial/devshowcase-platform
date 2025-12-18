using System.Security.Claims;
using DevShowCase.API.Data;
using DevShowCase.API.Models;
using DevShowcase.Shared.DTOs.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Public: Send a message
    [HttpPost]
    public async Task<IActionResult> SendMessage(ContactMessageDto dto)
    {
        // For now we just save to DB. Later we can add Email service.
        var message = new ContactMessage
        {
            Name = dto.Name,
            Email = dto.Email,
            Message = dto.Message,
            SentDate = DateTime.UtcNow,
            IsRead = false
        };

        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // Admin: List messages
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AdminContactMessageDto>>> GetMessages()
    {
        // In a multi-tenant app, messages should likely be linked to a user.
        // For now, this is a simple global list or we could filter by recipient if we add that field.
        var messages = await _context.ContactMessages
            .OrderByDescending(m => m.SentDate)
            .ToListAsync();

        return Ok(messages.Select(m => new AdminContactMessageDto
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            Message = m.Message,
            SentDate = m.SentDate,
            IsRead = m.IsRead
        }));
    }

    [HttpPatch("{id}/read")]
    [Authorize]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message == null) return NotFound();

        message.IsRead = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
