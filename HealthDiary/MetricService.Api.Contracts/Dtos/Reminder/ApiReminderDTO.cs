using System.ComponentModel.DataAnnotations;

namespace MetricService.Api.Contracts.Dtos.Reminder
{
    /// <summary>
    /// Объект данных о напоминании приема медикаментов пользователем
    /// </summary   
    public record ApiReminderDTO
    {
        /// <summary>
        /// Идентификатор схемы приема лекарств
        /// </summary>        
        public int RegimenId { get; init; }

        /// <summary>
        /// Время напоминания
        /// </summary>        
        public DateTime RemindAt { get; init; }

        /// <summary>
        /// Признак, было ли отправлено напоминание
        /// </summary>
        /// <value>
        ///   <c>true</c> если напоминание доставлено; иначе, <c>false</c>.
        /// </value>
        public bool IsSend { get; init; }
    }
}
