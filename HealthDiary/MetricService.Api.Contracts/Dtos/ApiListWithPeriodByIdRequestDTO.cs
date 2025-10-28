namespace MetricService.Api.Contracts.Dtos
{
    /// <summary>
    /// Объект для получения данных по пользователю за период
    /// </summary>   
    public record ApiListWithPeriodByIdRequestDTO
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>        
        public DateTime BegDate { get; set; }

        /// <summary>
        /// Дата конца периода
        /// </summary>        
        public DateTime EndDate { get; set; }
    }
}
