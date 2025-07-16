namespace MetricService.BLL.DTO.AccessToMetrics
{
    /// <summary>
    /// Базовый объект данных о доступе к личным метрикам пользователя
    /// </summary>
    public class AccessToMetricsBaseDTO
    {
        /// <summary>
        /// Дата, до которой включительно действует доступ личным метрикам
        /// </summary>        
        public DateOnly? AccessExpirationDate { get; set; }

        /// <summary>
        /// Доступ к метрикам без ограничения по скрокам
        /// </summary>
        /// <value>
        ///   <c>true</c> если доступ к метрикам постоянный; иначе, <c>false</c>.
        /// </value>
        public bool IsPermanentAccess { get; set; }
    }
}
