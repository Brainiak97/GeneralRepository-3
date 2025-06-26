using MetricService.BLL.DTO.Reminder;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class ReminderMapper
    {
        public static ReminderCreateDTO ToReminderCreateDTO(this Reminder reminder)
        {
            return new ReminderCreateDTO
            {
                IsSend = reminder.IsSend,
                RegimenId = reminder.RegimenId,
                RemindAt = reminder.RemindAt,
            };
        }

        public static Reminder ToReminder(this ReminderCreateDTO reminderCreateDTO)
        {
            return new Reminder
            {
                Id = 0,
                RemindAt = reminderCreateDTO.RemindAt,
                RegimenId = reminderCreateDTO.RegimenId,
                IsSend = reminderCreateDTO.IsSend,
            };
        }

        public static ReminderUpdateDTO ToReminderUpdateDTO(this Reminder reminder)
        {
            return new ReminderUpdateDTO
            {
                IsSend = reminder.IsSend,
                RemindAt = reminder.RemindAt,
                Id = reminder.Id,
            };
        }

        public static Reminder ToReminder(this ReminderUpdateDTO reminderUpdateDTO, int regimenId)
        {
            return new Reminder
            {
                Id = reminderUpdateDTO.Id,
                RemindAt = reminderUpdateDTO.RemindAt,
                IsSend = reminderUpdateDTO.IsSend,
                RegimenId = regimenId,
            };
        }

        public static ReminderDTO ToReminderDTO(this Reminder reminder)
        {
            return new ReminderDTO
            {
                Id = reminder.Id,
                RegimenId = reminder.RegimenId,
                IsSend = reminder.IsSend,
                RemindAt = reminder.RemindAt
            };
        }

        public static Reminder ToReminder(this ReminderDTO reminderDTO)
        {
            return new Reminder
            {
                Id= reminderDTO.Id,
                RemindAt= reminderDTO.RemindAt,
                IsSend = reminderDTO.IsSend,
                RegimenId= reminderDTO.RegimenId,                
            };
        }

        public static IEnumerable<ReminderDTO> ToReminderDTO(this IEnumerable<Reminder> reminders)
        {
            var result = new List<ReminderDTO>();

            foreach (var reminder in reminders)
            {
                result.Add(ToReminderDTO(reminder));
            }
            return result;
        }
    }
}
