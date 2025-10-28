namespace MetricService.Api.Contracts.Dtos.Reminder
{
    /// <summary>
    ///  Объект для получения данных о напоминаниях по схеме приема медикаментов за период
    /// </summary>    
    public record ApiRequestListWithPeriodByRegimenIdDTO
    {
        /// <summary>
        /// Идентификатор данных схема приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }

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
