using MetricService.Api.Contracts.Dtos.PhysicalActivity;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы со справочником "Физическая активность"
    /// </summary>    
    public interface IPhysicalActivityServiceClient
    {        
        const string Controller="/PhysicalActivity" ;
        /// <summary>
        /// Зарегисрировать новую физичекую активность в справочнике "Физическая активность"
        /// </summary>
        /// <param name="physicalActivityCreateDTO">Данные для регистрации физической активности</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreatePhysicalActivity)}")]
        Task CreatePhysicalActivity(PhysicalActivityCreateDTO physicalActivityCreateDTO);

        /// <summary>
        /// Изменить данные физической активности в справочнике "Физическая активность"
        /// </summary>
        /// <param name="physicalActivityUpdateDTO">Данные для изменения физической активности</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdatePhysicalActivity)}")]
        Task UpdatePhysicalActivity(PhysicalActivityUpdateDTO physicalActivityUpdateDTO);

        /// <summary>
        /// Удалить физическую активность из справочника "Физическая активность"
        /// </summary>
        /// <param name="physicalActivityId">Идентификатор данных физической активности</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeletePhysicalActivity)}")]
        Task DeletePhysicalActivity(int physicalActivityId);

        /// <summary>
        /// Получить список физических активностей из справочника "Физическая активность"
        /// </summary>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetAllPhysicalActivities)}")]
        Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivities();

        /// <summary>
        /// Поучить физическую активность из справочника "Физическая активность"
        /// </summary>
        /// <param name="physicalActivityId">Идентификатор данных физической активности</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetPhysicalActivityById)}")]
        Task<PhysicalActivityDTO> GetPhysicalActivityById(int physicalActivityId);

        /// <summary>
        /// Получить из справочника "Физическая активность" все подходящие физические активности по строке поиска
        /// </summary>
        /// <param name="search">Строка поиска. Для множественного поиска, фразы в строке разделяйте запятой</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(FindPhysicalActivityByName)}")]
        Task<IEnumerable<PhysicalActivityDTO>> FindPhysicalActivityByName(string search);
    }
}
