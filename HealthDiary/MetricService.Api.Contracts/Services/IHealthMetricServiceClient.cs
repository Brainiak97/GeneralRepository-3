using MetricService.Api.Contracts.Dtos.HealthMetric;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с показателями здоровья пользователя
    /// </summary>     
    public interface IHealthMetricServiceClient
    {
        const string Controller = "/HealthMetric";
        /// <summary>
        /// Зарегистрировать новый показатель здоровья пользователя
        /// </summary>
        /// <param name="healthMetricCreateDTO">Данные для регистрации нового показателя здоровья пользователя</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateHealthMetric)}")]
        Task CreateHealthMetric(HealthMetricCreateDTO healthMetricCreateDTO);

        /// <summary>
        /// Изменить данные о показателе здоровья пользователя
        /// </summary>
        /// <param name="healthMetricUpdateDTO">Данные для изменения показателя здоровья пользователя</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateHealthMetric)}")]
        Task UpdateHealthMetric(HealthMetricUpdateDTO healthMetricUpdateDTO);

        /// <summary>
        /// Удалить данные о показателе здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор показателя здоровья пользователя</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteHealthMetric)}")]
        Task DeleteHealthMetric(int healthMetricId);

        /// <summary>
        /// Получить список показателей здоровья пользователя
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllHealthMetrics)}")]
        Task<IEnumerable<HealthMetricDTO>> GetAllHealthMetrics();

        /// <summary>
        /// Получить показатель здоровья пользователя
        /// </summary>
        /// <param name="healthMetricId">Идентификатор показателя здоровья пользователя</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetHealthMetricById)}")]
        Task<HealthMetricDTO> GetHealthMetricById(int healthMetricId);
    }
}
