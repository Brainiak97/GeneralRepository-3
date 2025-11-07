namespace UserService.Api.Contracts.Dtos
{
    /// <summary>
    /// Представляет данные ответа при успешной аутентификации пользователя.
    /// Содержит имя пользователя, email и JWT-токен.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// Получает или задаёт имя пользователя.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Получает или задаёт email пользователя.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Получает или задаёт JWT-токен, используемый для аутентификации и авторизации.
        /// </summary>
        public required string Token { get; set; }
    }
}