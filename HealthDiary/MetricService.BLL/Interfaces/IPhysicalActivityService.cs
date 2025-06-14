using MetricService.Domain.Models;
using MetricService.BLL.Exceptions;
using MetricService.BLL.DTO.PhysicalActivity;

namespace MetricService.BLL.Interfaces
{
    public interface IPhysicalActivityService 
    {
        /// <summary>
        /// Создание физической активности
        /// </summary>
        /// <param name="physicalActivityCreateDTO">физическая активность</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>        
        /// <exception cref="ValidateModelException">физическая активность уже зарегистрирована</exception>
        public Task CreatePhysicalActivityAsync(PhysicalActivityCreateDTO physicalActivityCreateDTO);

        /// <summary>
        /// Обновить данные о физической активностие
        /// </summary>
        /// <param name="physicalActivityUpdateDTO">физическая активность</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>        
        /// <exception cref="ValidateModelException">физическая активность не зарегистрирована</exception>
        public Task UpdatePhysicalActivityAsync(PhysicalActivityUpdateDTO physicalActivityUpdateDTO);

        /// <summary>
        /// Удалить физическую активность
        /// </summary>
        /// <param name="physicalActivityId">ИД активности/param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная физическая активность не существует</exception>
        public Task DeletePhysicalActivityAsync(int physicalActivityId);

        /// <summary>
        /// Получить запись о физической автивности по ИД
        /// </summary>
        /// <param name="activityId">ИД физ. активности</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная физическая активность не существует</exception>
        public Task<PhysicalActivityDTO> GetPhysicalActivityByIdAsync(int activityId);

       /// <summary>
       /// получение списка физической активности
       /// </summary>
       /// <param name="pageNum">номер страницы для пагинации</param>
       /// <param name="pageSize">количество позиций на странице</param>
       /// <returns></returns>
        public Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync(int pageNum, int pageSize);


        /// <summary>
        /// получение списка физической активности, заданной строкой поиска по наименованию.
        /// В строку поиска по наименованию можно передать несколько фраз поиска через ","
        /// </summary>
        /// <param name="search">строка поиска по наименованию</param>
        /// <returns></returns>
        public Task<IEnumerable<PhysicalActivityDTO>> GetListPhysicalActivitiesBySearchAsync(string search);
    }
}
