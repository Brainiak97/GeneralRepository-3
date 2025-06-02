using UserService.Domain.Models;

namespace UserService.BLL.Dto
{
    /// <summary>
    /// Представляет данные, необходимые для регистрации нового пользователя.
    /// Содержит личные данные и учетные данные пользователя.
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// Получает или задаёт имя пользователя (логин), которое будет использоваться для входа.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Получает или задаёт email пользователя. Используется для связи и подтверждения аккаунта.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Получает или задаёт пароль пользователя для аутентификации.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Получает или задаёт имя пользователя (имя при регистрации).
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Получает или задаёт фамилию пользователя.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Получает или задаёт номер телефона пользователя.
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Получает или задаёт дату рождения пользователя.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Получает или задаёт пол пользователя.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Получает или задаёт хэш пароля пользователя.
        /// Используется для внутренней проверки и сохранения в базе данных.
        /// </summary>
        public required string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Указывает, был ли подтвержден email пользователя.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Указывает, заблокирован ли пользователь.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}