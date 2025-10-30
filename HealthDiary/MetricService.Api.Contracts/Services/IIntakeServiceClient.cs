using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.Intake;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данные о приеме лекарств пользователем
    /// </summary>    
    public interface IIntakeServiceClient
    {
        const string Controller = "/Intake";
        /// <summary>
        /// Зарегистрировать прием лекарств
        /// </summary>
        /// <param name="createDTO">Данные для регисрации о приеме лекарств</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateIntake)}")]
        Task CreateIntake(IntakeCreateDTO createDTO);

        /// <summary>
        /// Изменить данные примема лекарств
        /// </summary>
        /// <param name="updateDTO">Измененные данные приема лекарств</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateIntake)}")]
        Task UpdateIntake(IntakeUpdateDTO updateDTO);

        /// <summary>
        /// Удалить данные о приеме лекарств
        /// </summary>
        /// <param name="intakeId">Идентификатор приема лекарств</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteIntake)}")]
        Task DeleteIntake(int intakeId);

        /// <summary>
        /// Получить все приемы лекарств по пользователю за период
        /// </summary>
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllIntakes)}")]
        Task<IEnumerable<IntakeDTO>> GetAllIntakes(RequestListWithPeriodByIdDTO requestDTO);

        /// <summary>
        /// Получить данные приема лекарств
        /// </summary>
        /// <param name="intakeId">Идентификатор данных приема лекарств</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetIntakeById)}")]
        Task<IntakeDTO> GetIntakeById(int intakeId);
    }
}
