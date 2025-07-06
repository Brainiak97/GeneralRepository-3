namespace MetricService.BLL.DTO
{
    /// <summary>
    /// Объект для получения данных о напоминаниях по схеме приема медикаментов за период
    /// </summary>
    public class RequestListWithPeriodByRegimenIdDTO
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
