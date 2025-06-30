using UserService.Domain.Models;

namespace UserService.BLL.Dto
{
    /// <summary>
    /// Представляет данные для обновления информации о пользователе.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Получает или задаёт имя пользователя (имя при регистрации).
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Получает или задаёт фамилию пользователя.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Получает или задаёт email-адрес пользователя.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Получает или задаёт номер телефона пользователя.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// Получает или задаёт дату рождения пользователя.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Получает или задаёт пол пользователя.
        /// </summary>
        public Gender Gender { get; set; }
    }
}
