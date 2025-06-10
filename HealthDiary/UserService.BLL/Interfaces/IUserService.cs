using UserService.BLL.Dto;

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
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        Task<AuthResponseDto> Register(RegisterRequestDto request);

        /// <summary>
        /// Выполняет вход пользователя по имени пользователя (или email) и паролю.
        /// </summary>
        /// <param name="request">Объект <see cref="LoginRequestDto"/>, содержащий учетные данные пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="AuthResponseDto"/> с данными аутентификации.</returns>
        Task<AuthResponseDto> Login(LoginRequestDto request);

        /// <summary>
        /// Находит пользователя по его email-адресу.
        /// </summary>
        /// <param name="email">Email-адрес пользователя для поиска.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        Task<UserDto?> FindByEmail(string email);

        /// <summary>
        /// Подтверждает email пользователя.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, который нужно подтвердить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ConfirmEmailAsync(string email);

        /// <summary>
        /// Сбрасывает пароль пользователя на новый.
        /// </summary>
        /// <param name="email">Email-адрес пользователя, чей пароль нужно изменить.</param>
        /// <param name="newPassword">Новый пароль, который будет установлен.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ResetPassword(string email, string newPassword);

        /// <summary>
        /// Находит пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, чью информацию требуется отправить.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденного пользователя в виде <see cref="UserDto"/> или null, если пользователь не найден.</returns>
        Task<UserDto?> FindById(int userId);

        /// <summary>
        /// Выдает список всех пользователей в системе.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает найденных пользователей в виде списка <see cref="UserDto"/> или null, если пользователи не найдены.</returns>
        Task<IEnumerable<UserDto?>> GetAll();

        /// <summary>
        /// Обновляет данные пользователя в хранилище.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="dto">Dto модель обновленных данных пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> UpdateUserAsync(int userId, UserUpdateDto dto);

        /// <summary>
        /// Отмечает пользователя как удаленного.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> DeleteUserAsync(int userId);

        /// <summary>
        /// Восстановление пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> RestoreUserAsync(int userId);

        /// <summary>
        /// Блокировка и разблокировка пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="isBlocked">Указывает нужно заблокировать или разблокировать пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает положительный или отрицательный результат.</returns>
        Task<bool> BlockUserAsync(int userId, bool isBlocked);
    }
}