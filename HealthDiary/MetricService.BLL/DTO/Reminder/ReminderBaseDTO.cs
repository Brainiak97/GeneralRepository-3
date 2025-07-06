namespace MetricService.BLL.DTO.Reminder
{
    /// <summary>
    /// Объект базовых данных о напоминании приема медикаментов пользователем
    /// </summary>
    public abstract class ReminderBaseDTO
    {
        /// <summary>
        /// Время напоминания
        /// </summary>       
        public DateTime RemindAt { get; set; }

        /// <summary>
        /// Признак, было ли отправлено напоминание
        /// </summary>        
        public bool IsSend { get; set; }
    }
}
