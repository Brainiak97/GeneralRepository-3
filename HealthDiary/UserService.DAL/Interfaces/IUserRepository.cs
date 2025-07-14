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
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="username">Имя пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно получает пользователя по его email-адресу.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="email">Email-адрес для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно добавляет нового пользователя в хранилище.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="user">Пользователь, которого нужно добавить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает добавленного пользователя.</returns>
        Task<User> AddAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно получает роль по её названию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="roleName">Название роли для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденную роль или <see langword="null"/>, если роль не найдена.</returns>
        Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно назначает указанной роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task AssignRoleToUserAsync(int userId, int roleId, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно обновляет данные пользователя в хранилище.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="user">Объект пользователя с обновлёнными данными.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает обновлённого пользователя.</returns>
        Task<User> UpdateAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно получает пользователя по его идентификатору.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="userId">Идентификатор пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя или <see langword="null"/>, если пользователь не найден.</returns>
        Task<User?> FindByIdAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно получает всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденных пользователей или <see langword="null"/>, если пользователи не найдены.</returns>
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
    }
}