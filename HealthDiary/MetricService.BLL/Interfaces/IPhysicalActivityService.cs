using MetricService.BLL.Dto;
using MetricService.Domain.Models;

namespace MetricService.BLL.Interfaces
{
    public interface IPhysicalActivityService 
    {
       /// <summary>
       /// получение физической активности по ИД
       /// </summary>
       /// <param name="activityId">ИД активности</param>
       /// <returns></returns>
       public Task<PhysicalActivityDTO?> GetPhysicalActivityByIdAsync(int activityId);

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
        public Task<IEnumerable<PhysicalActivity>> GetListPhysicalActivitiesBySearchAsync(string search);
    }
}
