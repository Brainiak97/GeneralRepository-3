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
    }
}