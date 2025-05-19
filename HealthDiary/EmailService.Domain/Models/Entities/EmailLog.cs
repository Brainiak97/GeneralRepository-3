namespace EmailService.Domain.Models.Entities
{
    /// <summary>
    /// Лог отправленных электронных писем.
    /// Используется для аудита и мониторинга доставки писем.
    /// </summary>
    public class EmailLog
    {
        /// <summary>
        /// Уникальный идентификатор записи лога.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Адрес получателя письма.
        /// </summary>
        public required string To { get; set; }

        /// <summary>
        /// Тема отправленного письма.
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// Тело отправленного письма.
        /// </summary>
        public required string Body { get; set; }

        /// <summary>
        /// Указывает, было ли письмо успешно отправлено.
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// Дата и время отправки письма.
        /// </summary>
        public DateTime SentAt { get; set; }
    }
}
