namespace MetricService.Api.Contracts.Dtos.Reminder
{
    /// <summary>
    /// Объект для регистрации данных о напоминании приема медикаментов пользователем
    /// </summary>
    /// <param name="RegimenId">Идентификатор схемы приема лекарств</param>
    /// <param name="RemindAt">Время напоминания</param>  
    public record ApiReminderCreateRequestDTO
    {
        /// <summary>
        /// Идентификатор схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; init; }

        /// <summary>
        /// Время напоминания
        /// </summary>        
        public DateTime RemindAt { get; init; }
    }
}