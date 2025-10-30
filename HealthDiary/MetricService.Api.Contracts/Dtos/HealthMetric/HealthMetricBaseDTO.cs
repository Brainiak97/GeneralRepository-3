namespace MetricService.Api.Contracts.Dtos.HealthMetric
{
    /// <summary>
    /// Базовый объект данных показателей здоровья пользователя
    /// </summary>
    public abstract record HealthMetricBaseDTO
    {
        /// <summary>
        /// Наименование показателя (например, "Уровень стресса")
        /// </summary>         
        public required string Name { get; init; }

        /// <summary>
        /// Описание показателя (например, "Оценивайте стресс от 1 до 10")
        /// </summary>         
        public string? Description { get; init; }

        /// <summary>
        /// Единица измерения (шкала, проценты, абсолютные значения)
        /// </summary>        
        public string? Unit { get; init; }
    }
}
