namespace MetricService.BLL.DTO.Reminder
{
    /// <summary>
    /// Объект для изменения данных о напоминании приема медикаментов пользователем
    /// </summary>
    public class ReminderUpdateDTO: ReminderBaseDTO
    {
        /// <summary>
        /// Идентификатор данных о напоминании приема медикаментов пользователем
        /// </summary>
        public int Id { get; set; }       
    }
}
