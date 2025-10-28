namespace MetricService.Api.Contracts.Dtos.Enums
{
    /// <summary>
    /// Оценка состояния
    /// </summary>
    public enum ConditionRating
    {
        /// <summary>
        /// НЕ ОПРЕДЕЛЕНО
        /// </summary>
        None,

        /// <summary>
        /// УЖАСНО
        /// </summary>
        Terribly,

        /// <summary>
        /// ПЛОХО
        /// </summary>
        Badly,

        /// <summary>
        /// УДОВЛЕТВОРИТЕЛЬНО
        /// </summary>
        Satisfactory,

        /// <summary>
        /// ХОРОШО
        /// </summary>
        Well,

        /// <summary>
        /// ОТЛИЧНО
        /// </summary>
        Great,
    }
}
