namespace MetricService.BLL.DTO.Reminder
{
    public class ReminderDTO: ReminderBaseDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Схема приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }       
    }
}
