namespace MetricService.BLL.DTO.Sleep
{
    /// <summary>
    /// Объект данных о сне пользователя
    /// </summary>
    public class SleepDTO:SleepBaseDTO
    {
        /// <summary>
        /// Идентификатор данны о сне пользователя
        /// </summary>    
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
