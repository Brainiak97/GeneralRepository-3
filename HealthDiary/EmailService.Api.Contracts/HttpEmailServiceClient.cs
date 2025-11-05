using System.Net.Http.Headers;
using System.Net.Http.Json;
using EmailService.Api.Contracts.Dtos;
using Shared.EmailService.Common.Data;

namespace EmailService.Api.Contracts
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
        /// <inheritdoc />
        public async Task<bool> SendEmailAsync(EmailMessageData emailMessageData)
        {
            var form = GetMultiPartFormDataContent(emailMessageData);
            var response = await httpClient.PostAsync("api/email/SendEmail", form);
            return response.IsSuccessStatusCode;
        }

        /// <inheritdoc />
        public async Task<bool> SendEmailFromTemplateAsync(SendEmailFromTemplateDto dto)
        {
            var response = await httpClient.PostAsJsonAsync("api/email/SendFromTemplate", dto);
            return response.IsSuccessStatusCode;
        }

        private MultipartFormDataContent GetMultiPartFormDataContent(EmailMessageData emailMessageData)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(emailMessageData.To), nameof(emailMessageData.To));
            form.Add(new StringContent(emailMessageData.Subject), nameof(emailMessageData.Subject));
            form.Add(new StringContent(emailMessageData.Body), nameof(emailMessageData.Body));    

            foreach (var attachment in emailMessageData.Attachments)
            {
                var fileContent = new ByteArrayContent(attachment.Content);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.ContentType);
                form.Add(fileContent, nameof(emailMessageData.Attachments), attachment.FileName);
            }

            return form;
        }
    }
}