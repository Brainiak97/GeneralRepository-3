using UserService.Api.Contracts.Dtos;

namespace UserService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, предоставляющего функционал аутентификации и управления пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Регистрирует нового пользователя на основе предоставленных данных.
        /// </summary>
        /// <param name="request">Объект <see cref="RegisterRequestDto"/>, содержащий данные регистрации.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="isDoctor">Флаг для регистрации врача.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        Task<AuthResponseDto> Register(RegisterRequestDto request, CancellationToken cancellationToken, bool isDoctor = false);

        /// <summary>
        /// Выполняет вход пользователя по имени пользователя (или email) и паролю.
        /// </summary>
        /// <param name="request">Объект <see cref="LoginRequestDto"/>, содержащий учетные данные пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        Task<AuthResponseDto> Login(LoginRequestDto request, CancellationToken cancellationToken);

        /// <summary>
        /// Находит пользователя по его email-адресу.
        /// </summary>
        /// <param name="email">Email-адрес пользователя для поиска.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        Task<UserDto?> FindByEmail(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Подтверждает email пользователя.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, который нужно подтвердить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ConfirmEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Сбрасывает пароль пользователя на новый.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, чей пароль нужно изменить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="newPassword">Новый пароль, который будет установлен.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ResetPassword(string email, string newPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Находит пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чью информацию требуется отправить.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        Task<UserDto?> FindById(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Выдает список всех пользователей в системе.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденных пользователей в виде списка <see cref="UserDto"/> или null, если пользователи не найдены.</returns>
        Task<IEnumerable<UserDto?>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет данные пользователя в хранилище.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="dto">Dto модель обновленных данных пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> UpdateUserAsync(int userId, UserUpdateDto dto, CancellationToken cancellationToken);

        /// <summary>
        /// Отмечает пользователя как удаленного.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Восстановление пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> RestoreUserAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Блокировка и разблокировка пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="isBlocked">Указывает нужно заблокировать или разблокировать пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> BlockUserAsync(int userId, bool isBlocked, CancellationToken cancellationToken);

        /// <summary>
        /// Присоединяет указанную роль к указанному пользователю.
        /// </summary>
        /// <param name="roleName">Наименование роли</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task AssignRoleToUser(string roleName, int userId, CancellationToken cancellationToken);
    }
}