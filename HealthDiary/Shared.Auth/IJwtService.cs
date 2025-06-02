using System.Security.Claims;
using UserService.Domain.Models;

namespace Shared.Auth
{
    /// <summary>
    /// Определяет методы для работы с JWT-токенами: генерация и валидация.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Генерирует JWT-токен на основе данных пользователя и его ролей.
        /// </summary>
        /// <param name="user">Пользователь, для которого генерируется токен.</param>
        /// <param name="roles">Список ролей пользователя.</param>
        /// <returns>Сгенерированный JWT-токен в виде строки.</returns>
        string GenerateToken(User user, IEnumerable<Role> roles);
      
        /// <summary>
        /// Генерирует JWT-токен для конкретной цели (например, подтверждение email или сброс пароля).
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="email">Email пользователя.</param>
        /// <param name="purpose">Цель генерации токена (например, "email_verification", "password_reset").</param>
        /// <returns>Сгенерированный JWT-токен в виде строки.</returns>
        string GenerateToken(int userId, string email, string purpose);

        /// <summary>
        /// Проверяет корректность токена и возвращает содержимое, если он действителен.
        /// </summary>
        /// <param name="token">JWT-токен в виде строки.</param>
        /// <param name="expectedPurpose">Ожидаемая цель токена (необязательно).</param>
        /// <returns><see cref="ClaimsPrincipal"/> с данными из токена, если он валиден; иначе — null.</returns>
        ClaimsPrincipal? ValidateToken(string token, string? expectedPurpose);
    }
}