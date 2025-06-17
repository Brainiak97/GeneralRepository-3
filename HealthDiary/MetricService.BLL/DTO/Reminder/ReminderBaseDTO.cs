namespace MetricService.BLL.DTO.Reminder
{
    public  class ReminderBaseDTO
    {        
        /// <summary>
        /// Время напоминания
        /// </summary>       
        public DateTime RemindAt { get; set; }

        /// <summary>
        /// признак, было ли отправлено напоминание
        /// </summary>        
        public bool IsSend { get; set; }
    }
}
