using Microsoft.AspNetCore.Http;

namespace EmailService.Api.Contracts.Dtos
{
    /// <summary>
    /// Представляет данные, необходимые для отправки email на основе шаблона.
    /// </summary>
    public class SendEmailFromTemplateDto
    {
        /// <summary>
        /// Получает или задаёт название шаблона, который будет использоваться для формирования письма.
        /// </summary>
        public required string TemplateName { get; set; }

        /// <summary>
        /// Получает или задаёт словарь значений для замены плейсхолдеров в шаблоне.
        /// Ключ — имя плейсхолдера, значение — значение для замены.
        /// </summary>
        public required Dictionary<string, string> Placeholders { get; set; }

        /// <summary>
        /// Получает или задаёт email-адрес получателя.
        /// </summary>
        public required string To { get; set; }

        /// <summary>
        /// Вложения в письмо
        /// </summary>
        public List<IFormFile>? Attachments { get; set; }
    }
}