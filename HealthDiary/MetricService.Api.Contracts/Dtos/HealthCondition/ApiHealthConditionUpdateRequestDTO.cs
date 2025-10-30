namespace MetricService.Api.Contracts.Dtos.HealthCondition
{
    /// <summary>
    /// Объект для изменения данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>    
    public record ApiHealthConditionUpdateRequestDTO : ApiHealthConditionBaseDTO 
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; init; }       
    }
}
