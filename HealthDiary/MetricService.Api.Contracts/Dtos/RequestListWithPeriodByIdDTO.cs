namespace MetricService.Api.Contracts.Dtos
{
    /// <summary>
    /// Объект для получения данных по пользователю за период
    /// </summary>
    public record RequestListWithPeriodByIdDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Начало периода для выборки
        /// </summary>
        public DateTime BegDate { get; set; }

        /// <summary>
        /// Конец периода для выборки
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
