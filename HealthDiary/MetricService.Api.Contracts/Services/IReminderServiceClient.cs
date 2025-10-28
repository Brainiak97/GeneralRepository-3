using MetricService.Api.Contracts.Dtos;
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
        /// <param name="apiReminderCreateRequestDTO">Данные для регистрации напоминания о приеме лекарств</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateReminder)}")]
        Task CreateReminder(ApiReminderCreateRequestDTO apiReminderCreateRequestDTO);

        /// <summary>
        /// Изменить данные напоминаяния о приеме лекарств
        /// </summary>
        /// <param name="apiReminderUpdateRequestDTO">Измененные данные для напоминания оприеме лекарств</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateReminder)}")]
        Task UpdateReminder(ApiReminderUpdateRequestDTO apiReminderUpdateRequestDTO);

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
        /// <param name="apiListWithPeriodByIdRequestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllRemindersByUser)}")]
        Task<IEnumerable<ApiReminderDTO>> GetAllRemindersByUser(ApiListWithPeriodByIdRequestDTO apiListWithPeriodByIdRequestDTO);

        /// <summary>
        /// Получить список напоминаний по схеме приема медикаментов за период
        /// </summary>
        /// <param name="apiRequestListWithPeriodByRegimenIdDTO">Данные о схеме приема медикаментов и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllRemindersByRegimen)}")]
        Task<IEnumerable<ApiReminderDTO>> GetAllRemindersByRegimen(ApiRequestListWithPeriodByRegimenIdDTO apiRequestListWithPeriodByRegimenIdDTO);

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
