using MetricService.Api.Contracts.Dtos;
using MetricService.Api.Contracts.Dtos.HealthCondition;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с показателями самочувствия(состояния здоровья) пользователя
    /// </summary>    
    public interface IHealthConditionServiceClient
    {
        const string Controller = "/HealthCondition";
        /// <summary>
        /// Зарегистрировать новое значение самочувствия пользователя
        /// </summary>
        /// <param name="apiHealtConditionCreateRequest">Данные для регистрации нового значения самочувствия пользователя</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateHealthCondition)}")]
        Task CreateHealthCondition(ApiHealtConditionCreateRequest apiHealtConditionCreateRequest);

        /// <summary>
        /// Изменить данные о самочувствии(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="apiHealthConditionUpdateRequestDTO">Данные для изменения значения самочувствия(состояния здоровья) пользователя</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateHealthCondition)}")]
        Task UpdateHealthCondition(ApiHealthConditionUpdateRequestDTO apiHealthConditionUpdateRequestDTO);

        /// <summary>
        /// Удалить данные о самочувствия(состоянии здоровья) пользователя
        /// </summary>
        /// <param name="healthConditionId">Идентификатор значения самочувствия пользователя</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteHealthCondition)}")]
        Task DeleteHealthCondition(int healthConditionId);

        /// <summary>
        /// Получить список значений самочувствия пользователя
        /// </summary>
        /// <param name="apiListWithPeriodByIdRequestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllHealthConditions)}")]
        Task<IEnumerable<ApiHealthConditionDTO>> GetAllHealthConditions(ApiListWithPeriodByIdRequestDTO apiListWithPeriodByIdRequestDTO);

        /// <summary>
        /// Получить значение самочувствия пользователя
        /// </summary>
        /// <param name="healthConditionId">Идентификатор значения самочувствия пользователя</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetHealthConditionById)}")]
        Task<ApiHealthConditionDTO> GetHealthConditionById(int healthConditionId);
    }
}
