using MetricService.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MetricService.BLL.DTO.HealthCondition
{
    /// <summary>
    /// Объект для регистрации данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>
    /// <param name="UserId">ИД пользователя</param>
    /// <param name="RecordedAt">Дата и время записи</param>
    /// <param name="EmotionalState">Эмоциональное состояние</param>
    /// <param name="PhysicalState">Физическое состояние</param>
    /// <param name="Symptoms">Симптомы</param>
    /// <param name="Notes">Дополнительные заметки</param> 
    public record HealthConditionCreateDTO(            
         [Required] int UserId,
         [Required] DateTime RecordedAt,
         [Required] ConditionRating EmotionalState,
         [Required] ConditionRating PhysicalState,    
         string? Symptoms,      
         string? Notes 
        );
    
}
