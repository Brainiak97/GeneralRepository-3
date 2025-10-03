namespace MetricService.BLL.DTO.HealthCondition
{
    /// <summary>
    /// Объект данных о cамочувствии(состоянии здоровья) пользователя
    /// </summary>
    public class HealthConditionDTO : HealthConditionBaseDTO
    {
        /// <summary>
        /// Идентификатор данны о самочувствии(состоянии здоровья) пользователя        
        /// </summary>    
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
