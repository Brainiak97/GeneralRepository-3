namespace MetricService.BLL.DTO.HealthMetric
{
    /// <summary>
    /// Базовый объект данных значений показателей здоровья пользователя
    /// </summary>
    public abstract class HealthMetricValueBaseDTO
    {
        /// <summary>
        /// Ссылка на показатель здоровья
        /// </summary>        
        public int HealthMetricId { get; set; }

        /// <summary>
        /// Значение показателя
        /// </summary>         
        public float Value { get; set; }

        /// <summary>
        /// Дата и время записи
        /// </summary>         
        public DateTime RecordedAt { get; set; }

        /// <summary>
        /// Комментарий к записи
        /// </summary>       
        public string? Comment { get; set; }
    }
}
