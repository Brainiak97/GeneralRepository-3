namespace EmailService.BLL.Dto
{
    /// <summary>
    /// DTO для отправки письма на основе шаблона.
    /// </summary>
    public class SendEmailFromTemplateDto
    {
        /// <summary>
        /// Название шаблона письма (например, "RegistrationConfirmation").
        /// </summary>
        public required string TemplateName { get; set; }

        /// <summary>
        /// Словарь с подстановочными значениями для шаблона (например, {"username", "JohnDoe"}).
        /// </summary>
        public required Dictionary<string, string> Placeholders { get; set; }

        /// <summary>
        /// Адрес электронной почты получателя.
        /// </summary>
        public required string To { get; set; }
    }
}
