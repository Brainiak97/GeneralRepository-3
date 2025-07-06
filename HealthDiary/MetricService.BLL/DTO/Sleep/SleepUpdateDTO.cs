namespace MetricService.BLL.DTO.Sleep
{
    /// <summary>
    /// Объект для изменения данных о сне пользователя
    /// </summary>
    public class SleepUpdateDTO:SleepBaseDTO
    {
        /// <summary>
        /// Идентификатор данны о сне пользователя        
        /// </summary>    
        public int Id { get; set; } 
    }
}
