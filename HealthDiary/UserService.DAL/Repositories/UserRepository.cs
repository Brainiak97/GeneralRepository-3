using Microsoft.EntityFrameworkCore;
using UserService.DAL.EF;
using UserService.DAL.Interfaces;
using UserService.Domain.Models;

namespace UserService.DAL.Repositories
{
    public class UserRepository(UserServiceDbContext context) : IUserRepository
    {
        private readonly UserServiceDbContext _context = context;

        public async Task<User?> GetUserByUsernameAsync(string username)
        => await _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == username);

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
            => await _context.Roles
                        .FirstOrDefaultAsync(r => r.Name == roleName);

        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (user == null || role == null)
                throw new Exception("Пользователь или роль не найдены");

            user.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
