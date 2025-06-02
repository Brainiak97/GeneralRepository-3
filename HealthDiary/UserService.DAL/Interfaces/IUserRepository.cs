using UserService.Domain.Models;

namespace UserService.DAL.Interfaces
{
    /// <summary>
    /// Определяет контракт для репозитория, взаимодействующего с пользователями и ролями в базе данных.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Асинхронно получает пользователя по его имени пользователя (логину).
        /// </summary>
        /// <param name="username">Имя пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        Task<User?> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Асинхронно получает пользователя по его email-адресу.
        /// </summary>
        /// <param name="email">Email-адрес для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Асинхронно добавляет нового пользователя в хранилище.
        /// </summary>
        /// <param name="user">Пользователь, которого нужно добавить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает добавленного пользователя.</returns>
        Task<User> AddAsync(User user);

        /// <summary>
        /// Асинхронно получает роль по её названию.
        /// </summary>
        /// <param name="roleName">Название роли для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденную роль или <see langword="null"/>, если роль не найдена.</returns>
        Task<Role?> GetRoleByNameAsync(string roleName);

        /// <summary>
        /// Асинхронно назначает указанной роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task AssignRoleToUserAsync(int userId, int roleId);

        /// <summary>
        /// Асинхронно обновляет данные пользователя в хранилище.
        /// </summary>
        /// <param name="user">Объект пользователя с обновлёнными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает обновлённого пользователя.</returns>
        Task<User> UpdateAsync(User user);

        /// <summary>
        /// Асинхронно получает пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        Task<User?> FindByIdAsync(int userId);
    }
}