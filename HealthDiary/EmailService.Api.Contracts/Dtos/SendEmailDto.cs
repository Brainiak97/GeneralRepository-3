using Microsoft.AspNetCore.Http;

namespace EmailService.Api.Contracts.Dtos
{
    /// <summary>
    /// DTO для отправки произвольного электронного письма.
    /// </summary>
    public class SendEmailDto
    {
        /// <summary>
        /// Адрес электронной почты получателя.
        /// </summary>
        public required string To { get; set; }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// Тело письма в формате HTML.
        /// </summary>
        public required string Body { get; set; }

        /// <summary>
        /// Вложения в письмо
        /// </summary>
        public List<IFormFile> Attachments { get; set; } = [];
    }
}
