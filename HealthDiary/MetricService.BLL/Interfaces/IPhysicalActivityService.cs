using MetricService.Domain.Models;
using MetricService.BLL.Exceptions;
using MetricService.BLL.DTO.PhysicalActivity;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными справочника "Физическая активность"
    /// </summary>
    public interface IPhysicalActivityService 
    {
        /// <summary>
        /// Создать запись в справочнике
        /// </summary>
        /// <param name="physicalActivityCreateDTO">Данные для создания записи</param>              
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public Task CreatePhysicalActivityAsync(PhysicalActivityCreateDTO physicalActivityCreateDTO);

        /// <summary>
        /// Обновить запись в справочнике
        /// </summary>
        /// <param name="physicalActivityUpdateDTO">Данные для изменения</param>        
        /// <exception cref="IncorrectOrEmptyResultException"></exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public Task UpdatePhysicalActivityAsync(PhysicalActivityUpdateDTO physicalActivityUpdateDTO);

        /// <summary>
        /// Удалить запись в справочнике
        /// </summary>
        /// <param name="physicalActivityId">Идентификатор записи</param>        
        /// <exception cref="IncorrectOrEmptyResultException">Физическая активность не зарегистрирована</exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        public Task DeletePhysicalActivityAsync(int physicalActivityId);

        /// <summary>
        /// Получить запись из справочника
        /// </summary>
        /// <param name="activityId">Идентификатор записи</param>
        /// <returns>Данные записи из справочника</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная физическая активность не существует</exception>
        public Task<PhysicalActivityDTO> GetPhysicalActivityByIdAsync(int activityId);

        /// <summary>
        /// Получить список записей из справочника
        /// </summary>       
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync();


        /// <summary>
        /// Получить список записей из справочника по строке поиска.
        /// Разные фразы для поиска разделяются ","
        /// </summary>
        /// <param name="search">строка поиска. Для разделения фраз использовать ","</param>
        /// <returns>Список записей из справочника</returns>
        public Task<IEnumerable<PhysicalActivityDTO>> GetListPhysicalActivitiesBySearchAsync(string search);
    }
}
