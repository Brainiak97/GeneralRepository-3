using System.Security.Claims;
using UserService.Domain.Models;

namespace Shared.Auth
{
    public interface IJwtService
    {
        string GenerateToken(User user, IEnumerable<Role> roles);
        string GenerateToken(int userId, string email, string purpose);
        ClaimsPrincipal? ValidateToken(string token, string? expectedPurpose);
    }
}
