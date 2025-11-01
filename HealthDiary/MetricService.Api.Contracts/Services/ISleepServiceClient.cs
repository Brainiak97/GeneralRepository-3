using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.Sleep;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данными о сне пользователя
    /// </summary>
    public interface ISleepServiceClient
    {
        const string Controller = "/Sleep";
        /// <summary>
        /// Зарегистрировать данные о сне пользователя
        /// </summary>
        /// <param name="createDTO">Данные о сне пользователя</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateSleep)}")]
        Task CreateSleep(SleepCreateDTO createDTO);

        /// <summary>
        /// Изменить данные о сне пользователя
        /// </summary>
        /// <param name="updateDTO">Измененные данные о сне пользователя</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateSleep)}")]
        Task UpdateSleep(SleepUpdateDTO updateDTO);

        /// <summary>
        /// Удалить данные о сне пользователя
        /// </summary>
        /// <param name="sleepId">идентификатор сна пользователя</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteSleep)}")]
        Task DeleteSleep(int sleepId);

        /// <summary>
        /// Получить список данных о снах пользователя за период
        /// </summary>
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllSleeps)}")]
        Task<IEnumerable<SleepDTO>> GetAllSleeps(RequestListWithPeriodByIdDTO requestDTO);

        /// <summary>
        /// Получить данные о сне пользователя
        /// </summary>
        /// <param name="sleepId">Идентификатор данных сна пользователя</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetSleepById)}")]
        Task<SleepDTO> GetSleepById(int sleepId);
    }
}
