using EmailService.BLL.Dto;
using Microsoft.AspNetCore.Http;

namespace EmailService.BLL.Interfaces
{
    /// <summary>
    /// Определяет методы для отправки электронных писем.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет email, используя указанный шаблон и параметры замены.
        /// </summary>
        /// <param name="templateName">Название шаблона, который будет использоваться для формирования письма.</param>
        /// <param name="placeholders">Словарь значений для замены в шаблоне (например: { "username", "John" }).</param>
        /// <param name="to">Email-адрес получателя.</param>
        /// <param name="attachments">Вложения.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="EmailStatusResponseDto"/> с результатом отправки.</returns>
        Task<EmailStatusResponseDto> SendEmailFromTemplateAsync(string templateName, Dictionary<string, string>? placeholders, string to, List<IFormFile>? attachments);

        /// <summary>
        /// Отправляет email на указанный адрес с заданной темой и содержимым, включаяя вложения.
        /// </summary>
        /// <param name="to">Email-адрес получателя.</param>
        /// <param name="subject">Тема письма.</param>
        /// <param name="body">HTML-содержимое письма.</param>
        /// <param name="attachments">Вложения.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="EmailStatusResponseDto"/> с результатом отправки.</returns>
        Task<EmailStatusResponseDto> SendEmailAsync(string to, string subject, string body, List<IFormFile>? attachments);
    }
}