using EmailService.BLL.Dto;
using EmailService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Отправляет произвольное письмо пользователю.
        /// </summary>
        /// <param name="dto">DTO с данными получателя, темой и телом письма</param>
        [HttpPost("send")]
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
        [HttpPost("send-from-template")]
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
