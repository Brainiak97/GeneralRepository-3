namespace MetricService.Api.Contracts.Dtos.HealthCondition
{
    /// <summary>
    /// Объект данных о cамочувствии(состоянии здоровья) пользователя
    /// </summary>    
    public record ApiHealthConditionDTO : ApiHealthConditionBaseDTO 
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// ИД пользователя
        /// </summary>        
        public int UserId { get; init; }
    }
}
