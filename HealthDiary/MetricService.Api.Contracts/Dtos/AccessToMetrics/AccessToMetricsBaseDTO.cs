namespace MetricService.Api.Contracts.Dtos.AccessToMetrics
{
    /// <summary>
    /// Базовый объект данных о доступе к личным метрикам пользователя
    /// </summary>
    public abstract record AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Дата, до которой включительно действует доступ личным метрикам
        /// </summary>        
        public DateOnly? AccessExpirationDate { get; init; }

        /// <summary>
        /// Доступ к метрикам без ограничения по скрокам
        /// </summary>
        /// <value>
        ///   <c>true</c> если доступ к метрикам постоянный; иначе, <c>false</c>.
        /// </value>
        public bool IsPermanentAccess { get; init; }

        /// <summary>
        /// Идентификатор пользователя, которому предоставлен доступ к метрикам пользователя
        /// </summary>        
        public int GrantedUserId { get; init; }
    }
}
