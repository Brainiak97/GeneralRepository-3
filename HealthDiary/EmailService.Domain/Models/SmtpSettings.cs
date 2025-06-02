namespace EmailService.Domain.Models
{
    /// <summary>
    /// Представляет настройки подключения к SMTP-серверу для отправки электронных писем.
    /// </summary>
    public class SmtpSettings
    {
        /// <summary>
        /// Получает или задаёт хост SMTP-сервера.
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// Получает или задаёт порт SMTP-сервера.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Получает или задаёт имя пользователя (логин) для аутентификации на SMTP-сервере.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Получает или задаёт пароль для аутентификации на SMTP-сервере.
        /// </summary>
        public string? Password { get; set; }
    }
}