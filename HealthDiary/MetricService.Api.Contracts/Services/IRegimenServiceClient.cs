using MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.Regimen;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы со схемами приема лекарств
    /// </summary>    
    public interface IRegimenServiceClient
    {
        const string Controller = "/Regimen";
        /// <summary>
        /// Зарегистрировать схему приема лекарств
        /// </summary>
        /// <param name="createDTO">Данные для регистрации схемы приема лекарств</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateRegimen)}")]
        Task CreateRegimen(RegimenCreateDTO createDTO);

        /// <summary>
        /// Изменить данные схемы приема лекарств
        /// </summary>
        /// <param name="updateDTO">Данные для изменения схемы приема лекарств</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateRegimen)}")]
        Task UpdateRegimen(RegimenUpdateDTO updateDTO);

        /// <summary>
        /// Удалить схему приема лекарств
        /// </summary>
        /// <param name="regimenId">Идентификатор схемы</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteRegimenAsync)}")]
        Task DeleteRegimenAsync(int regimenId);

        /// <summary>
        /// Получить все схемы приема лекарств по пользователю за период
        /// </summary>
        /// <param name="requestDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllRegimens)}")]
        Task<IEnumerable<RegimenDTO>> GetAllRegimens(RequestListWithPeriodByIdDTO requestDTO);

        /// <summary>
        /// Получить схему приема лекарств
        /// </summary>
        /// <param name="regimenid">Идентификатор схемы приема лекарств</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetRegimenById)}")]
        Task<RegimenDTO> GetRegimenById(int regimenid);
    }
}
