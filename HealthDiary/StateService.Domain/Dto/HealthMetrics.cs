namespace StateService.Domain.Dto
{
    /// <summary>
    /// Упрощённая модель показателей здоровья для передачи в AI
    /// </summary>
    public class HealthMetrics
    {
        /// <summary>
        /// Дата измерения (без времени, если группируем по дням)
        /// </summary>        
        public DateTime MetricDate { get; set; }

        /// <summary>
        /// Название метрики (например, "Пульс", "Давление", "Уровень стресса")
        /// </summary>        
        public string MetricName { get; set; } = null!;

        /// <summary>
        /// Числовое значение показателя
        /// </summary>
        public float? Value { get; set; }

        /// <summary>
        /// Единица измерения (опционально, например: "уд/мин", "мм рт. ст.", "%")
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// Комментарий к записи (опционально)
        /// </summary>
        public string? Comment { get; set; }
    }
}
