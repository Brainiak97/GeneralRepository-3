using MetricService.Api.Contracts.Dtos.Enums;

namespace MetricService.Api.Contracts.Dtos.HealthCondition
{
    /// <summary>
    /// Объект данных о cамочувствии(состоянии здоровья) пользователя
    /// </summary>    
    public abstract record ApiHealthConditionBaseDTO
    {
        /// <summary>
        /// Дата и время записи
        /// </summary>       
        public DateTime RecordedAt { get; init; }

        /// <summary>
        /// Эмоциональное состояние
        /// </summary>        
        public ConditionRating EmotionalState { get; init; }

        /// <summary>
        /// Физическое состояние
        /// </summary>       
        public ConditionRating PhysicalState { get; init; }

        /// <summary>
        /// Симптомы
        /// </summary>       
        public string? Symptoms { get; init; }

        /// <summary>
        /// Дополнительные заметки
        /// </summary>       
        public string? Notes { get; init; }
    }
}
