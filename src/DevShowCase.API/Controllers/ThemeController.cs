using DevShowCase.API.Data;
using DevShowCase.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ThemeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ThemeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Public / Read-Only for everyone
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Theme>>> GetThemes()
    {
        return await _context.Themes
            .Where(t => t.IsActive)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Theme>> GetTheme(int id)
    {
        var theme = await _context.Themes.FindAsync(id);

        if (theme == null) return NotFound();

        return theme;
    }
}
