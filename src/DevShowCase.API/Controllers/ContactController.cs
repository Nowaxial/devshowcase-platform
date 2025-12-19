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

    // Public: Send a message to a specific user
    [HttpPost("{username}")]
    public async Task<IActionResult> SendMessage(string username, ContactMessageDto dto)
    {
        var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);
        if (targetUser == null) return NotFound("User not found");

        var message = new ContactMessage
        {
            UserId = targetUser.Id,
            Name = dto.Name,
            Email = dto.Email,
            Subject = dto.Subject,
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var messages = await _context.ContactMessages
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.SentDate)
            .ToListAsync();

        return Ok(messages.Select(m => new AdminContactMessageDto
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            Subject = m.Subject,
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
