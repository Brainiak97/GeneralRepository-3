namespace UserService.BLL.Dto
{
    /// <summary>
    /// Представляет данные, необходимые для входа пользователя в систему.
    /// Содержит имя пользователя и пароль.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Получает или задаёт имя пользователя (логин).
        /// Может быть также email-адресом, если система поддерживает вход через email.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задаёт пароль пользователя для аутентификации.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}