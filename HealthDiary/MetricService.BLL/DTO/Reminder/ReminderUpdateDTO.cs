using System.ComponentModel.DataAnnotations;

namespace MetricService.BLL.DTO.Reminder
{
    /// <summary>
    /// Объект для изменения данных о напоминании приема медикаментов пользователем
    /// </summary>
    /// <param name="Id">Идентификатор</param>    
    /// <param name="RemindAt">Время напоминания</param>   
    public record ReminderUpdateDTO(
        [Required] int Id,
        [Required] DateTime RemindAt
        );
}
