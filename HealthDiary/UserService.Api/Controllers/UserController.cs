using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.EmailClient;
using Shared.EmailClient.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserService.BLL.Dto;
using UserService.BLL.Interfaces;
using UserService.Domain.Models;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService, IEmailServiceClient emailServiceClient, IJwtService jwtService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IEmailServiceClient _emailServiceClient = emailServiceClient;
        private readonly string serviceLink = "https://localhost:7188/user/";

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await _userService.Register(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            // Только пользователи с ролью "Admin" могут получить доступ
            return Ok(new { Message = "Доступ разрешён для администраторов" });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            return Ok(new { UserId = userId, Roles = roles });
        }

        // POST: api/account/send-verification-email
        [HttpPost("send-verification-email")]
        public async Task<IActionResult> SendVerificationEmail([FromBody] string email)
        {
            var user = await _userService.FindByEmail(email);
            if (user == null || user.IsEmailConfirmed)
                return Ok(new { message = "Email подтвержден" });

            var token = GenerateEmailVerificationTokenAsync(user.Id, user.Email);
            var link = $"{serviceLink}verify-email?token={token}";

            var emailDto = new SendEmailDto
            {
                To = user.Email,
                Subject = "Подтвердите ваш email",
                Body = $"<p>Нажмите на ссылку ниже, чтобы подтвердить ваш email:</p><a href='{link}'>{link}</a>"
            };

            await _emailServiceClient.SendEmailAsync(emailDto);

            return Ok(new { message = "Письмо отправлено" });
        }

        // GET: api/account/verify-email
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var principal = jwtService.ValidateToken(token, "email_verification");
            if (principal == null)
                return BadRequest("Ссылка недействительна или истекла");

            var emailClaim = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            if (emailClaim == null)
                return BadRequest("Email не найден");

            await _userService.ConfirmEmailAsync(emailClaim.Value);

            return Ok(new { message = "Email подтвержден" });
        }

        // POST: api/account/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _userService.FindByEmail(email);
            if (user == null)
                return Ok();

            var token = GeneratePasswordResetTokenAsync(user.Id, user.Email);
            var resetLink = $"{serviceLink}reset-password?token={token}";

            var emailDto = new SendEmailFromTemplateDto
            {
                To = user.Email,
                TemplateName = "PasswordReset",
                Placeholders = new Dictionary<string, string>
                {
                    { "resetLink", resetLink },
                    { "username", user.Username }
                }
            };

            await _emailServiceClient.SendEmailFromTemplateAsync(emailDto);

            return Ok(new { message = "Письмо отправлено" });
        }

        // POST: api/account/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var principal = jwtService.ValidateToken(dto.Token, "password_reset");
            if (principal == null)
                return BadRequest("Ссылка недействительна или истекла");

            var emailClaim = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            if (emailClaim == null)
                return BadRequest("Email не найден");

            await _userService.ResetPassword(emailClaim.Value, dto.NewPassword);

            return Ok(new { message = "Пароль успешно изменён" });
        }

        // --- Вспомогательные методы ---

        private string GenerateEmailVerificationTokenAsync(int userId, string email)
        {
            return jwtService.GenerateToken(userId, email, "email_verification");
        }

        private string GeneratePasswordResetTokenAsync(int userId, string email)
        {
            return jwtService.GenerateToken(userId, email, "password_reset");
        }
    }
}
