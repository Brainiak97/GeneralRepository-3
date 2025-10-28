namespace MetricService.Api.Contracts.Dtos.PhysicalActivity
{
    /// <summary>
    /// Объект для изменения данных в справочнике "Физическая активность"
    /// </summary>
    public record PhysicalActivityUpdateDTO : PhysicalActivityBaseDTO
    {
        /// <summary>
        /// Идентификатор данных в справочнике
        /// </summary>
        public int Id { get; set; }
    }
}
