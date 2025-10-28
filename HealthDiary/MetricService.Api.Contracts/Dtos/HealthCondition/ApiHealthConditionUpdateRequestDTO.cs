using MetricService.Api.Contracts.Dtos.Enums;

namespace MetricService.Api.Contracts.Dtos.HealthCondition
{
    /// <summary>
    /// Объект для изменения данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>    
    public record ApiHealthConditionUpdateRequestDTO
    {
        /// <summary>
        /// ИД данных
        /// </summary>       
        public int Id { get; set; }

        /// <summary>
        /// Дата и время записи
        /// </summary>       
        public DateTime RecordedAt { get; set; }

        /// <summary>
        /// Эмоциональное состояние
        /// </summary>       
        public ConditionRating EmotionalState { get; set; }

        /// <summary>
        /// Физическое состояние
        /// </summary>
        public ConditionRating PhysicalState { get; set; }

        /// <summary>
        /// Симптомы
        /// </summary>        
        public string? Symptoms { get; set; }

        /// <summary>
        /// Дополнительные заметки
        /// </summary>        
        public string? Notes { get; set; }
    }
}
