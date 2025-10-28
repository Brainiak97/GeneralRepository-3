using System.ComponentModel.DataAnnotations;

namespace MetricService.BLL.DTO
{
    /// <summary>
    ///  Объект для получения данных о напоминаниях по схеме приема медикаментов за период
    /// </summary>
    /// <param name="RegimenId">Идентификатор данных схема приема лекарств</param>
    /// <param name="BegDate">Начало периода для выборки</param>
    /// <param name="EndDate">Конец периода для выборки</param>
    public record ApiRequestListWithPeriodByRegimenIdDTO(
        [Required] int RegimenId,
        [Required] DateTime BegDate,
        [Required] DateTime EndDate
        );
}
