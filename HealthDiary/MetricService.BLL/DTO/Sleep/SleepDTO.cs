namespace MetricService.BLL.DTO.Sleep
{
    public class SleepDTO:SleepBaseDTO
    {
        /// <summary>
        /// идентификатор
        /// </summary>    
        public int Id { get; set; }

        /// <summary>
        /// идентификатор пользователя
        /// </summary>        
        public int UserId { get; set; }
    }
}
