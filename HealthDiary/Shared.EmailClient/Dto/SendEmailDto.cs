namespace Shared.EmailClient.Dto
{
    /// <summary>
    /// Представляет данные, необходимые для отправки электронного письма.
    /// </summary>
    public class SendEmailDto
    {
        /// <summary>
        /// Получает или задаёт email-адрес получателя.
        /// </summary>
        public required string To { get; set; }

        /// <summary>
        /// Получает или задаёт тему письма.
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// Получает или задаёт содержимое письма (в формате HTML).
        /// </summary>
        public required string Body { get; set; }
    }
}