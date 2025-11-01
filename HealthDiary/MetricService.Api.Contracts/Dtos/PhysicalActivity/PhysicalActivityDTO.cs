namespace MetricService.Api.Contracts.Dtos.PhysicalActivity
{
    /// <summary>
    /// Объект данных в справочнике "Физическая активность"
    /// </summary>
    public record PhysicalActivityDTO : PhysicalActivityBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; init; }
    }
}
