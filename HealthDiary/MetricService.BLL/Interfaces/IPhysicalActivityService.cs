using MetricService.Domain.Models;
using MetricService.BLL.Exceptions;
using MetricService.BLL.DTO.PhysicalActivity;

namespace MetricService.BLL.Interfaces
{
    public interface IPhysicalActivityService 
    {
        /// <summary>
        /// Создание данных о физической активности
        /// </summary>
        /// <param name="physicalActivityCreateDTO"></param>
        /// <returns></returns>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public Task CreatePhysicalActivityAsync(PhysicalActivityCreateDTO physicalActivityCreateDTO);

        /// <summary>
        /// Обновление данных о физической активности
        /// </summary>
        /// <param name="physicalActivityUpdateDTO"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException"></exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public Task UpdatePhysicalActivityAsync(PhysicalActivityUpdateDTO physicalActivityUpdateDTO);

        /// <summary>
        /// Удаление данных о физической активности
        /// </summary>
        /// <param name="physicalActivityId"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Физическая активность не зарегистрирована</exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        public Task DeletePhysicalActivityAsync(int physicalActivityId);

        /// <summary>
        /// Получить запись о физической автивности по ИД
        /// </summary>
        /// <param name="activityId">ИД физ. активности</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная физическая активность не существует</exception>
        public Task<PhysicalActivityDTO> GetPhysicalActivityByIdAsync(int activityId);

        /// <summary>
        /// Получить все записи о физической активности
        /// </summary>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Кол-во строк на странице для пагинации</param>
        /// <returns></returns>
        public Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync(int pageNum, int pageSize);


        /// <summary>
        /// Получить список физической акстивности по строке поиска.
        /// Разные фразы для поиска разделяются ","
        /// </summary>
        /// <param name="search">строка поиска. Для разделения фраз использовать ","</param>
        /// <returns>Список</returns>
        public Task<IEnumerable<PhysicalActivityDTO>> GetListPhysicalActivitiesBySearchAsync(string search);
    }
}
