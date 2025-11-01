namespace MetricService.Api.Contracts.Dtos.Reminder
{
    /// <summary>
    /// Объект для изменеия данных о напоминании приема медикаментов пользователем
    /// </summary>    
    public record ApiReminderUpdateRequestDTO
    {
        /// <summary>
        /// Идентификатор напоминания
        /// </summary>        
        public int Id { get; init; }

        /// <summary>
        /// Время напоминания
        /// </summary>
        public DateTime RemindAt { get; init; }
    }
}