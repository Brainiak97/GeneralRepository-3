namespace MetricService.Api.Contracts.Dtos.Intake
{
    /// <summary>
    /// Объект данных о приеме лекарств пользователем
    /// </summary>
    public record IntakeDTO : IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о приеме лекарств пользователем
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }
    }
}
