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
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание показателя (например, "Оценивайте стресс от 1 до 10")
        /// </summary>         
        public string? Description { get; set; }

        /// <summary>
        /// Единица измерения (шкала, проценты, абсолютные значения)
        /// </summary>        
        public string? Unit { get; set; } = null!;
    }
}
