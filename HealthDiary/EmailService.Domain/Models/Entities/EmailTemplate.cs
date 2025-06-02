namespace EmailService.Domain.Models.Entities
{
    /// <summary>
    /// Шаблон электронного письма.
    /// Используется для хранения шаблонов писем, таких как: подтверждение email, восстановление пароля и т.д.
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Уникальный идентификатор шаблона.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название шаблона (например, "RegistrationConfirmation", "PasswordReset").
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// Тело письма в формате HTML с возможностью использования placeholder'ов (например, {{username}}).
        /// </summary>
        /// <example>
        /// Пример шаблона письма:
        /// <code><![CDATA[
        /// <div style="font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;">
        ///     <h2 style="color: #007BFF;">Запрос на сброс пароля</h2>
        ///     <p>Здравствуйте, {{username}}!</p>
        ///     <p>Мы получили запрос на сброс пароля для вашей учетной записи. Если это были не вы — проигнорируйте это письмо.</p>
        ///     <p>Для сброса пароля нажмите на кнопку ниже:</p>
        ///     <p style="text-align: center; margin: 24px 0;">
        ///         <a href="{{resetLink}}" style="background-color: #007BFF; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block;">Сбросить пароль</a>
        ///     </p>
        ///     <p>Если кнопка не работает, скопируйте ссылку ниже в адресную строку браузера:</p>
        ///     <p style="word-break: break-all;">{{resetLink}}</p>
        ///     <p style="margin-top: 32px; font-size: 0.9em; color: #666;">С уважением,<br>Команда Health Diary</p>
        /// </div>
        /// ]]></code>
        /// </example>
        public required string Body { get; set; }
    }
}
