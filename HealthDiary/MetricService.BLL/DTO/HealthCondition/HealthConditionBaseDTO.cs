using MetricService.Domain.Models.Enums;

namespace MetricService.BLL.DTO.HealthCondition
{
    /// <summary>
    /// Объект базовых данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>
    public class HealthConditionBaseDTO
    {
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
