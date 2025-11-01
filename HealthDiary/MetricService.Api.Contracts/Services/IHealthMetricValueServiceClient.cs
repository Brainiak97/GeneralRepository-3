using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.HealthMetricValue;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с показателями здоровья пользователя
    /// </summary>
    /// <seealso cref="Controller" />    
    public interface IHealthMetricValueServiceClient
    {
        const string Controller = "/HealthMetricValue";
        /// <summary>
        /// Зарегистрировать новое значение показателя здоровья пользователя
        /// </summary>
        /// <param name="createDTO">Данные для регистрации нового значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateHealthMetricValue)}")]
        Task CreateHealthMetricValue(HealthMetricValueCreateDTO createDTO);

        /// <summary>
        /// Изменить данные о значении показателе здоровья пользователя
        /// </summary>
        /// <param name="updateDTO">Данные для изменения значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateHealthMetricValue)}")]
        Task UpdateHealthMetricValue(HealthMetricValueUpdateDTO updateDTO);

        /// <summary>
        /// Удалить данные о значении показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueId">Идентификатор значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteHealthMetricValue)}")]
        Task DeleteHealthMetricValue(int healthMetricValueId);

        /// <summary>
        /// Получить список значений показателей здоровья пользователя
        /// </summary>
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllHealthMetricsValue)}")]
        Task<IEnumerable<HealthMetricValueDTO>> GetAllHealthMetricsValue(RequestListWithPeriodByIdDTO requestDTO);

        /// <summary>
        /// Получить значение показателя здоровья пользователя
        /// </summary>
        /// <param name="healthMetricValueId">Идентификатор значения показателя здоровья пользователя</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetHealthMetricValueById)}")]
        Task<HealthMetricValueDTO> GetHealthMetricValueById(int healthMetricValueId);
    }
}
