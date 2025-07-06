namespace MetricService.BLL.DTO.Sleep
{
    /// <summary>
    /// Объект для регистрации данных о сне пользователя
    /// </summary>
    public class SleepCreateDTO :SleepBaseDTO
    {        
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }        
    }
}
