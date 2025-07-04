namespace MetricService.BLL.DTO
{
    public  class RequestListWithPeriodByIdDTO
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId {  get; set; }

        /// <summary>
        /// начало периода для выборки
        /// </summary>
        public DateTime BegDate { get; set; }

        /// <summary>
        /// конец периода для выборки
        /// </summary>
        public DateTime EndDate { get; set; }       
    }
}
