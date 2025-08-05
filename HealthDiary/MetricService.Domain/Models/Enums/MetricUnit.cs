namespace MetricService.Domain.Models.Enums
{
    /// <summary>
    /// Единица измерения пользовательских показателей здоровья    
    /// </summary>
    public enum MetricUnit
    {
        /// <summary>
        /// Абсолютное значение (например, вес в кг)
        /// </summary>
        AbsoluteNumber,

        /// <summary>
        /// Шкала (например, от 1 до 10)
        /// </summary>
        Scale,

        /// <summary>
        /// Проценты (%)
        /// </summary>
        Percentage
    }
}
