using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;
using Shared.EmailClient;
using Shared.EmailClient.Dto;
using System.Security.Claims;
using UserService.BLL.Dto;
using UserService.BLL.Interfaces;

namespace UserService.Api.Controllers
{
    /// <summary>
    /// Предоставляет API-методы для обновления данных, подтверждения email и восстановления пароля.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService, IEmailServiceClient emailServiceClient, IJwtService jwtService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IEmailServiceClient _emailServiceClient = emailServiceClient;
        private readonly string serviceLink = "https://localhost:7188/user/";

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Сообщение о доступе (пример ответа).</returns>
        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Возвращает информацию о пользователе.
        /// </summary>
        /// <returns>Информация о пользователе.</returns>
        [HttpGet("GetUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfoAsync([FromBody] int userId)
        {
            var user = await _userService.FindById(userId);

            return Ok(user);
        }

        /// <summary>
        /// Отправляет письмо с подтверждением email на указанный адрес.
        /// </summary>
        /// <param name="email">Email пользователя для проверки.</param>
        /// <returns>Статус отправки письма.</returns>
        [HttpPost("SendVerificationEmail")]
        public async Task<IActionResult> SendVerificationEmail([FromBody] string email)
        {
            var user = await _userService.FindByEmail(email);
            if (user == null || user.IsEmailConfirmed)
                return Ok(new { message = "Email подтвержден" });

            var token = GenerateEmailVerificationTokenAsync(user.Id, user.Email);
            var link = $"{serviceLink}VerifyEmail?token={token}";

            var emailDto = new SendEmailDto
            {
                To = user.Email,
                Subject = "Подтвердите ваш email",
                Body = $"<p>Нажмите на ссылку ниже, чтобы подтвердить ваш email:</p><a href='{link}'>{link}</a>"
            };

            await _emailServiceClient.SendEmailAsync(emailDto);

            return Ok(new { message = "Письмо отправлено" });
        }

        /// <summary>
        /// Подтверждает email пользователя по токену.
        /// </summary>
        /// <param name="token">JWT-токен подтверждения email.</param>
        /// <returns>Результат подтверждения email.</returns>
        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var principal = jwtService.ValidateToken(token, "email_verification");
            if (principal == null)
                return BadRequest("Ссылка недействительна или истекла");

            var emailClaim = principal.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
                return BadRequest("Email не найден");

            await _userService.ConfirmEmailAsync(emailClaim.Value);

            return Ok(new { message = "Email подтвержден" });
        }

        /// <summary>
        /// Запрашивает сброс пароля и отправляет ссылку на email.
        /// </summary>
        /// <param name="email">Email пользователя, для которого нужно сбросить пароль.</param>
        /// <returns>Статус отправки письма со ссылкой для сброса.</returns>
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _userService.FindByEmail(email);
            if (user == null)
                return Ok();

            var token = GeneratePasswordResetTokenAsync(user.Id, user.Email);
            var resetLink = $"{serviceLink}ResetPassword?token={token}";

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

        /// <summary>
        /// Сбрасывает пароль пользователя по токену.
        /// </summary>
        /// <param name="dto">Объект, содержащий токен и новый пароль.</param>
        /// <returns>Результат изменения пароля.</returns>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var principal = jwtService.ValidateToken(dto.Token, "password_reset");
            if (principal == null)
                return BadRequest("Ссылка недействительна или истекла");

            var emailClaim = principal.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
                return BadRequest("Email не найден");

            await _userService.ResetPassword(emailClaim.Value, dto.NewPassword);

            return Ok(new { message = "Пароль успешно изменён" });
        }

        /// <summary>
        /// Обновляет данные пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="dto">Dto модель обновленных данных пользователя.</param>
        [HttpPut("UpdateUser/{id}")]
        [Authorize(Policy = "SelfOrAdmin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _userService.UpdateUserAsync(id, dto);

            if (!success)
                return NotFound("Пользователь не найден.");

            return NoContent();
        }

        /// <summary>
        /// Блокирует пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        [HttpPut("BlockUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _userService.BlockUserAsync(id, isBlocked: true);

            if (!success)
                return NotFound("Пользователь не найден.");

            return NoContent();
        }

        /// <summary>
        /// Разблокирует пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        [HttpPut("UnblockUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _userService.BlockUserAsync(id, isBlocked: false);

            if (!success)
                return NotFound("Пользователь не найден.");

            return NoContent();
        }

        /// <summary>
        /// Удаляет пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Policy = "SelfOrAdmin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);

            if (!success)
                return NotFound("Пользователь не найден.");

            return NoContent();
        }

        /// <summary>
        /// Восстанавливает пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        [HttpDelete("RestoreUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestoreUser(int id)
        {
            var success = await _userService.RestoreUserAsync(id);

            if (!success)
                return NotFound("Пользователь не найден.");

            return NoContent();
        }

        /// <summary>
        /// Генерирует JWT-токен для подтверждения email.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="email">Email пользователя.</param>
        /// <returns>Сгенерированный токен.</returns>
        private string GenerateEmailVerificationTokenAsync(int userId, string email)
        {
            return jwtService.GenerateToken(userId, email, "email_verification");
        }

        /// <summary>
        /// Генерирует JWT-токен для сброса пароля.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="email">Email пользователя.</param>
        /// <returns>Сгенерированный токен.</returns>
        private string GeneratePasswordResetTokenAsync(int userId, string email)
        {
            return jwtService.GenerateToken(userId, email, "password_reset");
        }
    }
}
