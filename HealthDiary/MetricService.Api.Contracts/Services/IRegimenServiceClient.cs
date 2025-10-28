using MetricService.Api.Contracts.Dtos;
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
        /// <param name="regimenCreateDTO">Данные для регистрации схемы приема лекарств</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateRegimen)}")]
        Task CreateRegimen(RegimenCreateDTO regimenCreateDTO);

        /// <summary>
        /// Изменить данные схемы приема лекарств
        /// </summary>
        /// <param name="regimenUpdateDTO">Данные для изменения схемы приема лекарств</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateRegimen)}")]
        Task UpdateRegimen(RegimenUpdateDTO regimenUpdateDTO);

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
        /// <param name="requestListWithPeriodByIdDTO">Данные пользователя и период</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllRegimens)}")]
        Task<IEnumerable<RegimenDTO>> GetAllRegimens(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO);

        /// <summary>
        /// Получить схему приема лекарств
        /// </summary>
        /// <param name="regimenid">Идентификатор схемы приема лекарств</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetRegimenById)}")]
        Task<RegimenDTO> GetRegimenById(int regimenid);
    }
}
