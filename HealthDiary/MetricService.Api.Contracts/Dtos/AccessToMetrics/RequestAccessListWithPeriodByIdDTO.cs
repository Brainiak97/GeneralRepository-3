namespace MetricService.Api.Contracts.Dtos.AccessToMetrics
{
    /// <summary>
    /// Объект для получения данных по пользователю, периоду и типу записей
    /// </summary>
    public record RequestAccessListWithPeriodByIdDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Выводить все данные о предоставлении доступа к личным метрикам или только активные(действует доступ)
        /// </summary>
        /// <value>
        ///   <c>true</c> Если выводить все данные; иначе <c>false</c>.
        /// </value>
        public bool AllRecords { get; init; }
    }
}
