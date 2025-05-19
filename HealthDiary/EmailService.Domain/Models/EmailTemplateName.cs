namespace EmailService.Domain.Models
{
    /// <summary>
    /// Типы шаблонов писем, используемых в системе.
    /// </summary>
    public enum EmailTemplateName
    {
        /// <summary>
        /// Подтверждение регистрации пользователя.
        /// </summary>
        RegistrationConfirmation,

        /// <summary>
        /// Восстановление пароля.
        /// </summary>
        PasswordReset,

        /// <summary>
        /// Общее уведомление системы.
        /// </summary>
        GeneralNotification
    }
}
