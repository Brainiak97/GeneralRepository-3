using Microsoft.EntityFrameworkCore;
using UserService.DAL.EF;
using UserService.DAL.Interfaces;
using UserService.Domain.Models;

namespace UserService.DAL.Repositories
{
    /// <summary>
    /// Предоставляет реализацию репозитория для работы с пользователями и ролями в базе данных.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных для взаимодействия с таблицами пользователей и ролей.</param>
        public UserRepository(UserServiceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно получает пользователя по его имени пользователя (логину), включая его роли.
        /// </summary>
        /// <param name="username">Имя пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        public async Task<User?> GetUserByUsernameAsync(string username)
            => await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Username == username);

        /// <summary>
        /// Асинхронно получает пользователя по его email-адресу, включая его роли.
        /// </summary>
        /// <param name="email">Email-адрес для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

        /// <summary>
        /// Асинхронно получает пользователя по его email-адресу, включая его роли.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        public async Task<User?> FindByIdAsync(int userId)
            => await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

        /// <summary>
        /// Асинхронно добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="user">Пользователь, которого нужно добавить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает добавленного пользователя.</returns>
        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Асинхронно получает роль по её названию.
        /// </summary>
        /// <param name="roleName">Название роли для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденную роль или <see langword="null"/>, если роль не найдена.</returns>
        public async Task<Role?> GetRoleByNameAsync(string roleName)
            => await _context.Roles
                        .FirstOrDefaultAsync(r => r.Name == roleName);

        /// <summary>
        /// Асинхронно назначает указанной роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="Exception">Выбрасывается, если пользователь или роль не найдены.</exception>
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

        /// <summary>
        /// Асинхронно обновляет данные пользователя в базе данных.
        /// </summary>
        /// <param name="user">Объект пользователя с обновлёнными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает обновлённого пользователя.</returns>
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}