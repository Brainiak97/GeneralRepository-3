namespace MetricService.Api.Contracts.Dtos.HealthMetricValue
{
    /// <summary>
    /// Базовый объект данных значений показателей здоровья пользователя
    /// </summary>
    public abstract record HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Ссылка на показатель здоровья
        /// </summary>        
        public int HealthMetricId { get; init; }

        /// <summary>
        /// Значение показателя
        /// </summary>         
        public float Value { get; init; }

        /// <summary>
        /// Дата и время записи
        /// </summary>         
        public DateTime RecordedAt { get; init; }

        /// <summary>
        /// Комментарий к записи
        /// </summary>       
        public string? Comment { get; init; }
    }
}
