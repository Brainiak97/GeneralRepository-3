using EmailService.BLL.Dto;
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

            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        /// <summary>
        /// Отправляет произвольное письмо с вложениями пользователю.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("SendEmailWithAttachments")]
        public async Task<IActionResult> SendEmailWithAttachments([FromForm] SendEmailWithAttachmentDto dto)
        {
            if (dto.Attachments == null || dto.Attachments.Count == 0)
                return BadRequest("Необходимо прикрепить хотя бы один файл.");

            var result = await _emailService.SendEmailWithAttachmentAsync(
                dto.To, dto.Subject, dto.Body, dto.Attachments);

            return result.Success ? Ok(result) : StatusCode(500, result);
        }

        /// <summary>
        /// Отправляет email на основе шаблона (например, подтверждение регистрации).
        /// </summary>
        /// <param name="dto">DTO с названием шаблона, значениями для подстановки и адресом</param>
        [HttpPost("SendFromTemplate")]
        public async Task<IActionResult> SendFromTemplate([FromBody] SendEmailFromTemplateDto dto)
        {
            var result = await _emailService.SendEmailFromTemplateAsync(dto.TemplateName, dto.Placeholders, dto.To);

            return result.Success ? Ok(result) : StatusCode(500, result);
        }
    }
}
