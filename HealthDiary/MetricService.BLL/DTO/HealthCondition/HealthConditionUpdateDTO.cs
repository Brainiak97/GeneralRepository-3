namespace MetricService.BLL.DTO.HealthCondition
{
    /// <summary>
    /// Объект для изменения данных о самочувствии(состоянии здоровья) пользователя
    /// </summary>
    public class HealthConditionUpdateDTO : HealthConditionBaseDTO
    {
        /// <summary>
        /// Идентификатор данны о самочувствии(состоянии здоровья) пользователя        
        /// </summary>    
        public int Id { get; set; }
    }
}
