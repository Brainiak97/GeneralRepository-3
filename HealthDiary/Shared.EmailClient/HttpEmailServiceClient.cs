using Shared.EmailClient.Dto;
using System.Net.Http.Json;

namespace Shared.EmailClient
{
    public class HttpEmailServiceClient : IEmailServiceClient
    {
        private readonly HttpClient _httpClient;

        public HttpEmailServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendEmailAsync(SendEmailDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/email/send", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailFromTemplateAsync(SendEmailFromTemplateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/email/send-from-template", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
