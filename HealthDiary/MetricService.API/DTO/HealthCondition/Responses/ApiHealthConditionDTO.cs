using MetricService.Domain.Models.Enums;

namespace MetricService.API.DTO.HealthCondition.Responses
{
    /// <summary>
    /// Объект данных о cамочувствии(состоянии здоровья) пользователя
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="UserId">ИД пользователя</param>
    /// <param name="RecordedAt">Дата и время записи</param>
    /// <param name="EmotionalState">Эмоциональное состояние</param>
    /// <param name="PhysicalState">Физическое состояние</param>
    /// <param name="Symptoms">Симптомы</param>
    /// <param name="Notes">Дополнительные заметки</param> 
    public record ApiHealthConditionDTO(
        int Id,
        int UserId,
        DateTime RecordedAt,
        ConditionRating EmotionalState,
        ConditionRating PhysicalState,
        string? Symptoms,
        string? Notes);
}
