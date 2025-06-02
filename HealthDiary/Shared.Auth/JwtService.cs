using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Domain.Models;

namespace Shared.Auth
{
    /// <summary>
    /// Предоставляет реализацию сервиса для работы с JWT-токенами: генерация и валидация.
    /// </summary>
    /// <remarks>
    /// Инициализирует новый экземпляр <see cref="JwtService"/> с указанными настройками.
    /// </remarks>
    /// <param name="settings">Настройки JWT (ключ, аудитория, издатель и т. д.).</param>
    public class JwtService(JwtSettings settings) : IJwtService
    {
        private readonly JwtSettings _settings = settings;

        /// <summary>
        /// Генерирует JWT-токен на основе данных пользователя и его ролей.
        /// </summary>
        /// <param name="user">Пользователь, для которого генерируется токен.</param>
        /// <param name="roles">Список ролей пользователя.</param>
        /// <returns>Сгенерированный JWT-токен в виде строки.</returns>
        public string GenerateToken(User user, IEnumerable<Role> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Генерирует JWT-токен для конкретной цели (например, подтверждение email или сброс пароля).
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="email">Email пользователя.</param>
        /// <param name="purpose">Цель генерации токена (например, "email_verification", "password_reset").</param>
        /// <returns>Сгенерированный JWT-токен в виде строки.</returns>
        public string GenerateToken(int userId, string email, string purpose)
        {
            var claims = new List<Claim>
            {
                new("sub", userId.ToString()),
                new(ClaimTypes.Email, email),
                new("purpose", purpose)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Проверяет корректность токена и возвращает содержимое, если он действителен.
        /// </summary>
        /// <param name="token">JWT-токен в виде строки.</param>
        /// <param name="expectedPurpose">Ожидаемая цель токена (необязательно).</param>
        /// <returns><see cref="ClaimsPrincipal"/> с данными из токена, если он валиден; иначе — null.</returns>
        public ClaimsPrincipal? ValidateToken(string token, string? expectedPurpose)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = "HealthDiary.UserService",
                    ValidateAudience = true,
                    ValidAudience = "HealthDiary"
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var purposeClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "purpose");

                if (purposeClaim?.Value != expectedPurpose)
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}