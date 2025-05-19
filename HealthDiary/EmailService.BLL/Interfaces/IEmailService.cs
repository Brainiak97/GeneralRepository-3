using EmailService.BLL.Dto;

namespace EmailService.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<EmailStatusResponseDto> SendEmailAsync(string to, string subject, string body);
        Task<EmailStatusResponseDto> SendEmailFromTemplateAsync(string templateName, Dictionary<string, string> placeholders, string to);
    }
}
