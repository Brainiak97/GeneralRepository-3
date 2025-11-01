namespace MetricService.Api.Contracts.Dtos.Regimen
{
    /// <summary>
    /// Объект данных схемы приема медикаментов пользователем
    /// </summary>
    public record RegimenDTO : RegimenBaseDTO
    {
        /// <summary>
        /// Идентификатор данных схемы приема медикаментов пользователем
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; init; }

        /// <summary>
        /// Идентификатор данных из справочника "Медикаменты"
        /// </summary>       
        public int MedicationId { get; init; }
    }
}
