using System.ComponentModel.DataAnnotations;

namespace MetricService.API.DTO.HealthCondition.Responses
{
    /// <summary>
    /// Объект данных о напоминании приема медикаментов пользователем
    /// </summary
    /// <param name="RegimenId">Идентификатор схемы приема лекарств</param>
    /// <param name="RemindAt">Время напоминания</param>
    /// <param name="IsSend">Признак, было ли отправлено напоминание</param>
    public record ApiReminderDTO(
        [Required] int RegimenId,
        [Required] DateTime RemindAt,
        [Required] bool IsSend
        );
}
