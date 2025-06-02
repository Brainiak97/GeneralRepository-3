using Shared.EmailClient.Dto;
using System.Net.Http.Json;

namespace Shared.EmailClient
{
    /// <summary>
    /// Реализация клиента email-сервиса, использующая HTTP-запросы для взаимодействия с API.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр <see cref="HttpEmailServiceClient"/>.
    /// </remarks>
    /// <param name="httpClient">HTTP-клиент для выполнения запросов к email-сервису.</param>
    public class HttpEmailServiceClient(HttpClient httpClient) : IEmailServiceClient
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <summary>
        /// Асинхронно отправляет email, используя предоставленные данные.
        /// </summary>
        /// <param name="dto">Объект <see cref="SendEmailDto"/>, содержащий данные для отправки письма.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see langword="true"/>, если письмо было успешно отправлено; иначе — <see langword="false"/>.</returns>
        public async Task<bool> SendEmailAsync(SendEmailDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/email/SendEmail", dto);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Асинхронно отправляет email, используя указанный шаблон и параметры замены.
        /// </summary>
        /// <param name="dto">Объект <see cref="SendEmailFromTemplateDto"/>, 
        /// содержащий название шаблона, плейсхолдеры и адрес получателя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. 
        /// Возвращает <see langword="true"/>, если письмо было успешно отправлено; иначе — <see langword="false"/>.</returns>
        public async Task<bool> SendEmailFromTemplateAsync(SendEmailFromTemplateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/email/SendFromTemplate", dto);
            return response.IsSuccessStatusCode;
        }
    }
}