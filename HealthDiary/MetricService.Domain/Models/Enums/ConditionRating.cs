namespace MetricService.Domain.Models.Enums
{
    /// <summary>
    /// Оценка состояния
    /// </summary>
    public enum ConditionRating
    {
        /// <summary>
        /// Оценка состояния НЕ ОПРЕДЕЛЕНО
        /// </summary>
        None,

        /// <summary>
        /// Оценка состояния УЖАСНО
        /// </summary>
        Terribly,

        /// <summary>
        /// Оценка состояния ПЛОХО
        /// </summary>
        Badly,

        /// <summary>
        /// Оценка состояния УДОВЛЕТВОРИТЕЛЬНО
        /// </summary>
        Satisfactory,

        /// <summary>
        /// Оценка состояния ХОРОШО
        /// </summary>
        Well,

        /// <summary>
        /// Оценка состояния ОТЛИЧНО
        /// </summary>
        Great,
    }
}
