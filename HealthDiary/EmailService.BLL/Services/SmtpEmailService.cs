﻿using EmailService.BLL.Dto;
using EmailService.BLL.Interfaces;
using EmailService.DAL.Interfaces;
using EmailService.Domain.Models;
using EmailService.Domain.Models.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailService.BLL.Services
{
    /// <summary>
    /// Реализация сервиса для отправки email через SMTP.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр <see cref="SmtpEmailService"/>.
    /// </remarks>
    /// <param name="smtpSettings">Настройки SMTP-сервера.</param>
    /// <param name="templateRepo">Репозиторий шаблонов писем.</param>
    /// <param name="logRepo">Репозиторий логов отправленных писем.</param>
    /// <param name="logger">Логгер для записи ошибок и информации.</param>
    public class SmtpEmailService(
        IOptions<SmtpSettings> smtpSettings,
        IRepository<EmailTemplate> templateRepo,
        IRepository<EmailLog> logRepo,
        ILogger<SmtpEmailService> logger) : IEmailService
    {
        private readonly SmtpSettings _smtpSettings = smtpSettings.Value;
        private readonly IRepository<EmailTemplate> _templateRepo = templateRepo;
        private readonly IRepository<EmailLog> _logRepo = logRepo;
        private readonly ILogger<SmtpEmailService> _logger = logger;

        /// <summary>
        /// Отправляет email на указанный адрес.
        /// </summary>
        /// <param name="to">Email получателя.</param>
        /// <param name="subject">Тема письма.</param>
        /// <param name="body">HTML-содержимое письма.</param>
        /// <returns><see cref="EmailStatusResponseDto"/> с результатом отправки.</returns>
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

        /// <summary>
        /// Отправляет email, используя заранее подготовленный шаблон.
        /// </summary>
        /// <param name="templateName">Название шаблона.</param>
        /// <param name="placeholders">Ключевые значения для замены в шаблоне.</param>
        /// <param name="to">Email получателя.</param>
        /// <returns><see cref="EmailStatusResponseDto"/> с результатом отправки.</returns>
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