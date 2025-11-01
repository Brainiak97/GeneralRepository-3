namespace MetricService.Api.Contracts.Dtos.Common
{
    /// <summary>
    /// Объект для получения данных по пользователю за период
    /// </summary>   
    public record ApiListWithPeriodByIdRequestDTO
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Дата начала периода
        /// </summary>        
        public DateTime BegDate { get; init; }

        /// <summary>
        /// Дата конца периода
        /// </summary>        
        public DateTime EndDate { get; init; }
    }
}
