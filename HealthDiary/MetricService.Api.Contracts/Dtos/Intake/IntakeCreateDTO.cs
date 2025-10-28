namespace MetricService.Api.Contracts.Dtos.Intake
{
    /// <summary>
    /// Объект для регистрации данных о приеме лекарств пользователем
    /// </summary>
    public record IntakeCreateDTO : IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }
    }
}
