using System.ComponentModel.DataAnnotations;

namespace MetricService.API.DTO.HealthCondition.Requests
{
    /// <summary>
    /// Объект для изменеия данных о напоминании приема медикаментов пользователем
    /// </summary>
    /// <param name="Id">Идентификатор напоминания</param>
    /// <param name="RemindAt">Время напоминания</param>  
    public record ApiReminderUpdateRequestDTO(
        [Required] int Id,
        [Required] DateTime RemindAt
        );
}