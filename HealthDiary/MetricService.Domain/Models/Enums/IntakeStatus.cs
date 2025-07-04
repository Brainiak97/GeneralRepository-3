namespace MetricService.Domain.Models.Enums
{
    /// <summary>
    /// Статус приема лекарств
    /// </summary>
    public enum IntakeStatus: ushort
    {
        /// <summary>
        /// Статус према лекарств не указан
        /// </summary>
        НеОпределено,
        /// <summary>
        /// Статус - лекарства приняты
        /// </summary>
        Принято,
        /// <summary>
        /// Статус - прием лекарств пропущен
        /// </summary>
        Пропущено,
        /// <summary>
        /// Статус - прием лекарств перенесен
        /// </summary>
        Перенесено
    }
}
