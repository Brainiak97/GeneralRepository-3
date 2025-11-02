using EmailService.Api.Contracts.Dtos;
using Shared.EmailService.Common.Data;

namespace EmailService.Api.Contracts
{
    /// <summary>
    /// Определяет контракт для клиента, взаимодействующего с email-сервисом через HTTP.
    /// </summary>
    public interface IEmailServiceClient
    {
        /// <summary>
        /// Асинхронно отправляет электронное письмо, используя предоставленные данные.
        /// </summary>
        /// <param name="emailMessageData">Данные электронного письма.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see langword="true"/>, если письмо было успешно отправлено; иначе — <see langword="false"/>.</returns>
        Task<bool> SendEmailAsync(EmailMessageData emailMessageData);

        /// <summary>
        /// Асинхронно отправляет электронное письмо, используя указанный шаблон и параметры замены.
        /// </summary>
        /// <param name="dto">Объект <see cref="SendEmailFromTemplateDto"/>, 
        /// содержащий название шаблона, словарь значений для замены и адрес получателя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see langword="true"/>, если письмо было успешно отправлено; иначе — <see langword="false"/>.</returns>
        Task<bool> SendEmailFromTemplateAsync(SendEmailFromTemplateDto dto);
    }
}