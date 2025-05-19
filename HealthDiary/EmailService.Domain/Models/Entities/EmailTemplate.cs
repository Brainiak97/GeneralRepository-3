namespace EmailService.Domain.Models.Entities
{
    /// <summary>
    /// Шаблон электронного письма.
    /// Используется для хранения шаблонов писем, таких как: подтверждение email, восстановление пароля и т.д.
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Уникальный идентификатор шаблона.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название шаблона (например, "RegistrationConfirmation", "PasswordReset").
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// Тело письма в формате HTML с возможностью использования placeholder'ов (например, {{username}}).
        /// </summary>
        public required string Body { get; set; }
    }
}
