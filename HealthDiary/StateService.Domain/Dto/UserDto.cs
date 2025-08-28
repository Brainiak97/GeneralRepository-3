using UserService.Domain.Models;

namespace StateService.Domain.Dto
{
    public class UserDto
    {
        /// <summary>
        /// Получает или задаёт уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задаёт email-адрес пользователя.
        /// </summary>
        public required string Email { get; set; }

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
    }
}
