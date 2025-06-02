namespace Shared.Auth
{
    /// <summary>
    /// Представляет настройки, используемые для конфигурации JWT-токенов.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Получает или задаёт секретный ключ, используемый для подписи и проверки JWT-токенов.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задаёт издателя (issuer) токена. Используется для проверки подлинности токена.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задаёт получателя (audience) токена. Используется для проверки, кому предназначен токен.
        /// </summary>
        public string Audience { get; set; } = string.Empty;
    }
}