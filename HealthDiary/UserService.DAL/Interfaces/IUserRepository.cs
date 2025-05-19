using UserService.Domain.Models;

namespace UserService.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task AssignRoleToUserAsync(int userId, int roleId);
    }
}
