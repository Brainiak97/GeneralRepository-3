using Shared.EmailClient.Dto;

namespace Shared.EmailClient
{
    public interface IEmailServiceClient
    {
        Task<bool> SendEmailAsync(SendEmailDto dto);
        Task<bool> SendEmailFromTemplateAsync(SendEmailFromTemplateDto dto);
    }
}
