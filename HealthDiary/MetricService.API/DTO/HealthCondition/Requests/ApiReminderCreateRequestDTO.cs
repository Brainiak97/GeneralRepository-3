using System.ComponentModel.DataAnnotations;

namespace MetricService.API.DTO.HealthCondition.Requests
{
    /// <summary>
    /// Объект для регистрации данных о напоминании приема медикаментов пользователем
    /// </summary>
    /// <param name="RegimenId">Идентификатор схемы приема лекарств</param>
    /// <param name="RemindAt">Время напоминания</param>  
    public record ApiReminderCreateRequestDTO(
        [Required] int RegimenId,
        [Required] DateTime RemindAt
        );
}