namespace EmailService.BLL.Dto
{
    /// <summary>
    /// Ответ API после попытки отправки письма.
    /// </summary>
    public class EmailStatusResponseDto
    {
        /// <summary>
        /// Указывает, была ли отправка письма успешной.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение с деталями результата отправки.
        /// </summary>
        public required string Message { get; set; }

        /// <summary>
        /// Опциональный идентификатор лога отправки письма.
        /// </summary>
        public int? LogId { get; set; }
    }
}
