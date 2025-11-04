namespace StateService.Domain.Dto
{
    public class RequestListWithPeriodById
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
