using Shared.EmailClient.Dto;

namespace Shared.EmailClient
{
    /// <summary>
    /// Определяет контракт для клиента, взаимодействующего с email-сервисом через HTTP.
    /// </summary>
    public interface IEmailServiceClient
    {
        /// <summary>
        /// Асинхронно отправляет электронное письмо, используя предоставленные данные.
        /// </summary>
        /// <param name="dto">Объект <see cref="SendEmailDto"/>, содержащий параметры письма: адрес получателя, тема, тело.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see langword="true"/>, если письмо было успешно отправлено; иначе — <see langword="false"/>.</returns>
        Task<bool> SendEmailAsync(SendEmailDto dto);

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