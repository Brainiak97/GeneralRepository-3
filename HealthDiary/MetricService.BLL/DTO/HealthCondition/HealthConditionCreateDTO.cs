namespace MetricService.BLL.DTO.HealthCondition
{
    /// <summary>
    /// Объект для регистрации данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>
    public class HealthConditionCreateDTO : HealthConditionBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
