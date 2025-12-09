using DevShowCase.API.Models;
using System.Security.Claims;
namespace DevShowcase.API.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user, IList<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}