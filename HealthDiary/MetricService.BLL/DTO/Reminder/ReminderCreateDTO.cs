namespace MetricService.BLL.DTO.Reminder
{
    public class ReminderCreateDTO: ReminderBaseDTO
    {       
        /// <summary>
        /// Схема приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }
    }
}
