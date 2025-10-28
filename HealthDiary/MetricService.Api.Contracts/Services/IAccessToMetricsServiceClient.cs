using MetricService.Api.Contracts.Dtos.AccessToMetrics;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными доступа к личным метрикам пользователя
    /// </summary>
    public interface IAccessToMetricsServiceClient
    {
        const string Controller = "/AccessToMetrics";
        /// <summary>
        /// Предоставить доступ к личным метрикам
        /// </summary>
        /// <param name="accessToMetricsCreateDTO">Данные для предоставления доступа</param>        
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateAccessToMetricsAsync)}")]
        Task CreateAccessToMetricsAsync(AccessToMetricsCreateDTO accessToMetricsCreateDTO);

        /// <summary>
        /// Изменить доступ к личным метрикам
        /// </summary>
        /// <param name="accessToMetricsUpdateDTO">Данные для изменения доступа к личным метрикам</param>        
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateAccessToMetricsAsync)}")]
        Task UpdateAccessToMetricsAsync(AccessToMetricsUpdateDTO accessToMetricsUpdateDTO);

        /// <summary>
        /// Удалить доступ к личным метрикам
        /// </summary>
        /// <param name="accessToMetricsId">Идентификатор записи доступа</param>       
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteAccessToMetricsAsync)}")]
        Task DeleteAccessToMetricsAsync(int accessToMetricsId);

        /// <summary>
        /// Получить список доступа к личным метрикам для пользователя, предоставившего доступ
        /// </summary>
        /// <param name="requestAccessListWithPeriodByIdDTO">Данные пользователя, период и типы записей</param>       
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllAccessToMetricsByProviderAsync)}")]
        Task<IEnumerable<AccessToMetricsDTO>> GetAllAccessToMetricsByProviderAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO);

        /// <summary>
        /// Получить список доступа к личным метрикам для пользователя, получившего доступ
        /// </summary>
        /// <param name="requestAccessListWithPeriodByIdDTO">Данные пользователя, период и типы записей</param>        
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllAccessToMetricsByGrantedAsync)}")]
        Task<IEnumerable<AccessToMetricsDTO>> GetAllAccessToMetricsByGrantedAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO);

    }
}
