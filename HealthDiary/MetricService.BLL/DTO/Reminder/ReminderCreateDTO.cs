namespace MetricService.BLL.DTO.Reminder
{
    /// <summary>
    /// Объект для регистрации данных о напоминании приема медикаментов пользователем
    /// </summary>
    public class ReminderCreateDTO: ReminderBaseDTO
    {
        /// <summary>
        ///  Идентификатор данных схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; set; }
    }
}
