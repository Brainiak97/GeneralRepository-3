using System.ComponentModel.DataAnnotations;

namespace MetricService.API.DTO.HealthCondition.Requests
{
    /// <summary>
    /// Объект для получения данных по пользователю за период
    /// </summary>
    /// <param name="UserId">ИД пользователя</param>
    /// <param name="BegDate">Дата начала периода</param>
    /// <param name="EndDate">Дата конца периода</param>
    public record ApiListWithPeriodByIdRequestDTO(
        [Required] int UserId,
        [Required] DateTime BegDate,
        [Required] DateTime EndDate);
}
