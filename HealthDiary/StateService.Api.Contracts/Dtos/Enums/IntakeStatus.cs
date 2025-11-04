namespace StateService.Api.Contracts.Dtos.Enums
{
    /// <summary>
    /// Статус приема лекарств
    /// </summary>
    public enum IntakeStatus: ushort
    {
        /// <summary>
        /// Статус према лекарств не указан
        /// </summary>
        None,
        /// <summary>
        /// Статус - лекарства приняты
        /// </summary>
        Accepted,
        /// <summary>
        /// Статус - прием лекарств пропущен
        /// </summary>
        Skipped,
        /// <summary>
        /// Статус - прием лекарств перенесен
        /// </summary>
        Rescheduled
    }
}
