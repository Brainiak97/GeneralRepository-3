namespace MetricService.Api.Contracts.Dtos.Medication
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Медикаменты"
    /// </summary>
    public record MedicationUpdateDTO : MedicationBaseDTO
    {
        /// <summary>
        /// Идентификатор данны в справочнике
        /// </summary>
        public int Id { get; init; }
    }
}
