﻿using EmailService.BLL.Dto;

namespace EmailService.BLL.Interfaces
{
    /// <summary>
    /// Определяет методы для отправки электронных писем.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет email на указанный адрес с заданной темой и содержимым.
        /// </summary>
        /// <param name="to">Email-адрес получателя.</param>
        /// <param name="subject">Тема письма.</param>
        /// <param name="body">HTML-содержимое письма.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="EmailStatusResponseDto"/> с результатом отправки.</returns>
        Task<EmailStatusResponseDto> SendEmailAsync(string to, string subject, string body);

        /// <summary>
        /// Отправляет email, используя указанный шаблон и параметры замены.
        /// </summary>
        /// <param name="templateName">Название шаблона, который будет использоваться для формирования письма.</param>
        /// <param name="placeholders">Словарь значений для замены в шаблоне (например: { "username", "John" }).</param>
        /// <param name="to">Email-адрес получателя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see cref="EmailStatusResponseDto"/> с результатом отправки.</returns>
        Task<EmailStatusResponseDto> SendEmailFromTemplateAsync(string templateName, Dictionary<string, string> placeholders, string to);
    }
}