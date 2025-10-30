namespace MetricService.Api.Contracts.Dtos.HealthCondition
{
    /// <summary>
    ///   Объект для регистрации данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>    
    public record ApiHealtConditionCreateRequest : ApiHealthConditionBaseDTO
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>        
        public int UserId { get; init; }
    }
}