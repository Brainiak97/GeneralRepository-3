using MetricService.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Самочувствие(состояние здоровья) пользователя
    /// </summary>
    [Comment("состояние здоровья пользователя")]
    public class HealthCondition : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary> 
        [Comment("Идентификатор пользователя")]
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>        
        public User User { get; set; } = null!;

        /// <summary>
        /// Дата и время записи
        /// </summary>  
        [Comment("Дата и время записи")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime RecordedAt { get; set; }

        /// <summary>
        /// Эмоциональное состояние
        /// </summary>        
        [Comment("Эмоциональное состояние")]
        public ConditionAssessment EmotionalState { get; set; }

        /// <summary>
        /// Физическое состояние
        /// </summary>
        [Comment("Физическое состояние")]
        public ConditionAssessment PhysicalState { get; set; }

        /// <summary>
        /// Симптомы
        /// </summary>
        [Comment("Симптомы")]
        public string? Symptoms { get; set; }

        /// <summary>
        /// Дополнительные заметки
        /// </summary>
        [Comment("Дополнительные заметки")]
        public string? Notes { get; set; }
    }
}
