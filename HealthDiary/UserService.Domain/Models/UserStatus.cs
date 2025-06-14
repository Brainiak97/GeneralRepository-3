namespace UserService.Domain.Models
{
    /// <summary>
    /// Представляет статус пользователя.
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// Активен.
        /// </summary>
        Active,
        /// <summary>
        /// Удален.
        /// </summary>
        Deleted,
        /// <summary>
        /// Заблокирован.
        /// </summary>
        Blocked
    }
}
