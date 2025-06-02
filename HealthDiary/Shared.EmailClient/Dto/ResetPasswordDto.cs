namespace Shared.EmailClient.Dto
{
    /// <summary>
    /// Представляет данные, необходимые для сброса пароля пользователя.
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// Получает или задаёт токен, используемый для подтверждения запроса на сброс пароля.
        /// </summary>
        public required string Token { get; set; }

        /// <summary>
        /// Получает или задаёт новый пароль, который будет установлен для пользователя.
        /// </summary>
        public required string NewPassword { get; set; }
    }
}