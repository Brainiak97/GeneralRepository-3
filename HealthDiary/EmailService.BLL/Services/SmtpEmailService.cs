using EmailService.BLL.Dto;
using EmailService.BLL.Interfaces;
using EmailService.DAL.Interfaces;
using EmailService.DAL.Repositories;
using EmailService.Domain.Models;
using EmailService.Domain.Models.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailService.BLL.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IRepository<EmailTemplate> _templateRepo;
        private readonly IRepository<EmailLog> _logRepo;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(
            IOptions<SmtpSettings> smtpSettings,
            IRepository<EmailTemplate> templateRepo,
            IRepository<EmailLog> logRepo,
            ILogger<SmtpEmailService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _templateRepo = templateRepo;
            _logRepo = logRepo;
            _logger = logger;
        }

        public async Task<EmailStatusResponseDto> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Health Diary", _smtpSettings.Username));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };
                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, false);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                var log = new EmailLog
                {
                    To = to,
                    Subject = subject,
                    Body = body,
                    IsSent = true,
                    SentAt = DateTime.UtcNow
                };

                await _logRepo.AddAsync(log);

                return new EmailStatusResponseDto
                {
                    Success = true,
                    Message = "Письмо успешно отправлено",
                    LogId = log.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отправке email");
                return new EmailStatusResponseDto
                {
                    Success = false,
                    Message = $"Ошибка: {ex.Message}"
                };
            }
        }

        public async Task<EmailStatusResponseDto> SendEmailFromTemplateAsync(string templateName, Dictionary<string, string> placeholders, string to)
        {
            var template = await _templateRepo.GetAllAsync()
                .ContinueWith(t => t?.Result?.FirstOrDefault(t => t.Name == templateName));

            if (template == null)
            {
                return new EmailStatusResponseDto
                {
                    Success = false,
                    Message = $"Шаблон '{templateName}' не найден"
                };
            }

            var body = template.Body;
            foreach (var placeholder in placeholders)
            {
                body = body.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return await SendEmailAsync(to, template.Subject, body);
        }
    }
}

