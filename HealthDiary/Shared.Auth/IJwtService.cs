using UserService.Domain.Models;

namespace Shared.Auth
{
    public interface IJwtService
    {
        string GenerateToken(User user, IEnumerable<Role> roles);
        bool ValidateToken(string token);
    }
}
