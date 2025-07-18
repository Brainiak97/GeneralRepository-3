﻿using EmailService.BLL.Dto;
using EmailService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Api.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для отправки сообщений на электронную почту (по шаблонам / по стандарту).
    /// </summary>
    /// <param name="emailService"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController(IEmailService emailService) : ControllerBase
    {
        private readonly IEmailService _emailService = emailService;

        /// <summary>
        /// Отправляет произвольное письмо пользователю.
        /// </summary>
        /// <param name="dto">DTO с данными получателя, темой и телом письма</param>
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDto dto)
        {
            var result = await _emailService.SendEmailAsync(dto.To, dto.Subject, dto.Body);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        /// <summary>
        /// Отправляет email на основе шаблона (например, подтверждение регистрации).
        /// </summary>
        /// <param name="dto">DTO с названием шаблона, значениями для подстановки и адресом</param>
        [HttpPost("SendFromTemplate")]
        public async Task<IActionResult> SendFromTemplate([FromBody] SendEmailFromTemplateDto dto)
        {
            var result = await _emailService.SendEmailFromTemplateAsync(dto.TemplateName, dto.Placeholders, dto.To);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
