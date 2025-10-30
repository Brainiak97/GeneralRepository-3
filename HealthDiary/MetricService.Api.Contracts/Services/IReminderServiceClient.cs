using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.Reminder;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с напоминаниями о приеме лекарств.
    /// </summary>    
    public interface IReminderServiceClient
    {
        const string Controller = "/Reminder";
        /// <summary>
        /// зарегистрировать напоминание о приеме лекарств
        /// </summary>
        /// <param name="createDTO">Данные для регистрации напоминания о приеме лекарств</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateReminder)}")]
        Task CreateReminder(ApiReminderCreateRequestDTO createDTO);

        /// <summary>
        /// Изменить данные напоминаяния о приеме лекарств
        /// </summary>
        /// <param name="updateDTO">Измененные данные для напоминания оприеме лекарств</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateReminder)}")]
        Task UpdateReminder(ApiReminderUpdateRequestDTO updateDTO);

        /// <summary>
        /// Удалить напоминание о приеме лекарств
        /// </summary>
        /// <param name="id">Идентификатор напоминания о приеме лекарств</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteReminderAsync)}")]
        Task DeleteReminderAsync(int id);

        /// <summary>
        /// Получить список напоминаний по пользователю за период
        /// </summary>
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllRemindersByUser)}")]
        Task<IEnumerable<ApiReminderDTO>> GetAllRemindersByUser(ApiListWithPeriodByIdRequestDTO requestDTO);

        /// <summary>
        /// Получить список напоминаний по схеме приема медикаментов за период
        /// </summary>
        /// <param name="requestDTO">Данные о схеме приема медикаментов и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllRemindersByRegimen)}")]
        Task<IEnumerable<ApiReminderDTO>> GetAllRemindersByRegimen(ApiRequestListWithPeriodByRegimenIdDTO requestDTO);

        /// <summary>
        /// Получить напоминание о приеме лекарств
        /// </summary>
        /// <param name="reminderid">Идентификатор напоминания</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetReminderById)}")]
        Task<ApiReminderDTO> GetReminderById(int reminderid);

        /// <summary>
        /// Доставить напоминаяния пользователю
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(ReminderDelivery)}")]
        Task<IEnumerable<ApiReminderDTO>> ReminderDelivery(int userId);
    }
}
