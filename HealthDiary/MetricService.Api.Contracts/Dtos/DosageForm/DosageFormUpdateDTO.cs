namespace MetricService.Api.Contracts.Dtos.DosageForm
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Форма выпуска препарата"
    /// </summary>
    public record DosageFormUpdateDTO : DosageFormBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; init; }
    }
}
