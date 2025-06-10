namespace UserService.Domain.Models
{
    /// <summary>
    /// Представляет модель пользователя в системе.
    /// Содержит личные данные, учетные данные и информацию о ролях пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Получает или задаёт уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задаёт имя пользователя (логин), используемое для аутентификации.
        /// </summary>
        public required string Username { get; set; }

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

        /// <summary>
        /// Получает или задаёт хэш пароля пользователя.
        /// </summary>
        public required string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Указывает, был ли подтвержден email пользователя.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Указывает активен, удален или заблокирован пользователь.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Получает или задает дату удаления пользователя
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Получает или задаёт дату и время создания учетной записи пользователя.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Получает или задаёт коллекцию ролей, назначенных пользователю.
        /// </summary>
        public ICollection<Role> Roles { get; set; } = [];
    }
}