namespace MetricService.BLL.DTO.Sleep
{
    public class SleepUpdateDTO:SleepBaseDTO
    {
        /// <summary>
        /// идентификатор
        /// 0 - для новых записей
        /// >0 - для существующих записей
        /// </summary>    
        public int Id { get; set; } 
    }

}
