using MetricService.Api.Contracts.Dtos.Enums;

namespace MetricService.Api.Contracts.Dtos.HealthCondition
{
    /// <summary>
    ///   Объект для регистрации данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>    
    public record ApiHealtConditionCreateRequest
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int UserId { get; set; }

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
