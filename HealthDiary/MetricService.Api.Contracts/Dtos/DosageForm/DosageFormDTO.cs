namespace MetricService.Api.Contracts.Dtos.DosageForm
{
    /// <summary>
    /// Объект данных в справочнике "Форма выпуска препарата"
    /// </summary>
    public record DosageFormDTO : DosageFormBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }
    }
}
