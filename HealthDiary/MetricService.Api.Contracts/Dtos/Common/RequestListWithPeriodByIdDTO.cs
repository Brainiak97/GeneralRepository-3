namespace MetricService.Api.Contracts.Dtos.Common
{
    /// <summary>
    /// Объект для получения данных по пользователю за период
    /// </summary>
    public record RequestListWithPeriodByIdDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Начало периода для выборки
        /// </summary>
        public DateTime BegDate { get; init; }

        /// <summary>
        /// Конец периода для выборки
        /// </summary>
        public DateTime EndDate { get; init; }
    }
}
