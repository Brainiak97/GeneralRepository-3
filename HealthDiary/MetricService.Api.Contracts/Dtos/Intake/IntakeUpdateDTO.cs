namespace MetricService.Api.Contracts.Dtos.Intake
{
    /// <summary>
    /// Объект для изменения данных о приеме лекарств пользователем
    /// </summary>
    public record IntakeUpdateDTO : IntakeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о приеме лекарств пользователем
        /// </summary>
        public int Id { get; set; }
    }
}
