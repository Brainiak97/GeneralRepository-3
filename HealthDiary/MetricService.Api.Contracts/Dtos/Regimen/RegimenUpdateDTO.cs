namespace MetricService.Api.Contracts.Dtos.Regimen
{
    /// <summary>
    /// Объект для изменения данных схема прима медикаментов пользователем
    /// </summary>
    public record RegimenUpdateDTO : RegimenBaseDTO
    {
        /// <summary>
        /// Идентификатор данных схемы приема медикаментов пользователем
        /// </summary>
        public int Id { get; set; }
    }
}
