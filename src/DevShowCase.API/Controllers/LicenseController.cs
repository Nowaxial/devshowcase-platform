using System.Security.Claims;
using DevShowCase.API.Data;
using DevShowCase.API.Models;
using DevShowcase.Shared.DTOs.Portfolio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevShowCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LicenseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LicenseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LicenseDto>>> GetLicenses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var licenses = await _context.Licenses
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.IssueDate)
            .ToListAsync();

        return Ok(licenses.Select(l => new LicenseDto
        {
            Id = l.Id,
            UserId = l.UserId,
            Name = l.Name,
            IssuingOrganization = l.Issuer ?? string.Empty,
            IssueDate = l.IssueDate ?? DateTime.Now,
            ExpirationDate = l.ExpiryDate,
            CredentialId = l.CredentialId,
            CredentialUrl = l.CredentialUrl,
            ContentLanguage = l.ContentLanguage
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LicenseDto>> GetLicense(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var license = await _context.Licenses
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        if (license == null) return NotFound();

        return new LicenseDto
        {
            Id = license.Id,
            UserId = license.UserId,
            Name = license.Name,
            IssuingOrganization = license.Issuer ?? string.Empty,
            IssueDate = license.IssueDate ?? DateTime.Now,
            ExpirationDate = license.ExpiryDate,
            CredentialId = license.CredentialId,
            CredentialUrl = license.CredentialUrl,
            ContentLanguage = license.ContentLanguage
        };
    }

    [HttpPost]
    public async Task<ActionResult<LicenseDto>> CreateLicense(CreateLicenseDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var license = new License
        {
            UserId = userId,
            Name = dto.Name,
            Issuer = dto.IssuingOrganization,
            IssueDate = dto.IssueDate,
            ExpiryDate = dto.ExpirationDate,
            CredentialId = dto.CredentialId,
            CredentialUrl = dto.CredentialUrl,
            ContentLanguage = dto.ContentLanguage
        };

        _context.Licenses.Add(license);
        await _context.SaveChangesAsync();

        var result = new LicenseDto
        {
            Id = license.Id,
            UserId = license.UserId,
            Name = license.Name,
            IssuingOrganization = license.Issuer ?? string.Empty,
            IssueDate = license.IssueDate ?? DateTime.Now,
            ExpirationDate = license.ExpiryDate,
            CredentialId = license.CredentialId,
            CredentialUrl = license.CredentialUrl,
            ContentLanguage = license.ContentLanguage
        };

        return CreatedAtAction(nameof(GetLicense), new { id = license.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLicense(int id, CreateLicenseDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var license = await _context.Licenses
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        if (license == null) return NotFound();

        license.Name = dto.Name;
        license.Issuer = dto.IssuingOrganization;
        license.IssueDate = dto.IssueDate;
        license.ExpiryDate = dto.ExpirationDate;
        license.CredentialId = dto.CredentialId;
        license.CredentialUrl = dto.CredentialUrl;
        license.ContentLanguage = dto.ContentLanguage;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLicense(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var license = await _context.Licenses
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

        if (license == null) return NotFound();

        _context.Licenses.Remove(license);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
