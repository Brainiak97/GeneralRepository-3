using MetricService.BLL.DTO.Reminder;
using MetricService.BLL.DTO;

namespace MetricService.BLL.Interfaces
{
    public interface IReminderService
    {
        public Task CreateReminderAsync(ReminderCreateDTO reminderCreateDTO);


        public Task UpdateReminderAsync(ReminderUpdateDTO reminderUpdateDTO);


        public Task DeleteReminderAsync(int reminderId);


        public Task<ReminderDTO> GetReminderByIdAsync(int reminderId);


        public Task<IEnumerable<ReminderDTO>> GetAllReminderByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);

        public Task<IEnumerable<ReminderDTO>> GetAllReminderByRegimenIdAsync(RequestListWithPeriodByRegimenIdDTO requestListWithPeriodByRegimenIdDTO);
    }
}
