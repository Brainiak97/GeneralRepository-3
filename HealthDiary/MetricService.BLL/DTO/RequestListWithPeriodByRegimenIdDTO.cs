namespace MetricService.BLL.DTO
{
    public class RequestListWithPeriodByRegimenIdDTO
    {
        /// <summary>
        /// Схема приема лекарств
        /// </summary>
        public int RegimenId { get; set; }

        /// <summary>
        /// начало периода для выборки
        /// </summary>
        public DateTime BegDate { get; set; }

        /// <summary>
        /// конец периода для выборки
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// номер страницы для пагинации
        /// </summary>
        public int NumPage { get; set; }

        /// <summary>
        /// количество строк на странице
        /// </summary>
        public int PageSize { get; set; }
    }
}
