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
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор данных из справочника "Медикаменты"
        /// </summary>       
        public int MedicationId { get; set; }
    }
}
