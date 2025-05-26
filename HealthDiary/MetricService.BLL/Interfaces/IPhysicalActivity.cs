using MetricService.BLL.Dto;
using MetricService.Domain.Models;

namespace MetricService.BLL.Interfaces
{
    public interface IPhysicalActivity 
    {
       /// <summary>
       /// получение физической активности по ИД
       /// </summary>
       /// <param name="activityId">ИД активности</param>
       /// <returns></returns>
       public Task<PhysicalActivity?> GetPhysicalActivityByIdAsync(int activityId);

       /// <summary>
       /// получение списка физической активности
       /// </summary>
       /// <param name="pageNum">номер страницы для пагинации</param>
       /// <param name="pageSize">количество позиций на странице</param>
       /// <returns></returns>
        public Task<IEnumerable<PhysicalActivity>> GetAllPhysicalActivitiesAsync(int pageNum, int pageSize);
    }
}
