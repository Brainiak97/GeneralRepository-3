namespace MetricService.BLL.DTO.Reminder
{
    /// <summary>
    /// Объект данных о напоминании приема медикаментов пользователем
    /// </summary>
    public class ReminderDTO: ReminderBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о напоминании приема медикаментов пользователем
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }       
    }
}
