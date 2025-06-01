using MetricService.BLL.Dto;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Services
{
    public  class PhysicalActivityService : IPhysicalActivityService
    {
        private readonly IPhysicalActivityRepository _repository;

        public PhysicalActivityService(IPhysicalActivityRepository physicalActivityRepository)
        {
            _repository = physicalActivityRepository;
        }


        /// <summary>
        /// Получить все записи о физической активности
        /// </summary>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Кол-во строк на странице для пагинации</param>
        /// <returns></returns>
        public async Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync(int pageNum, int pageSize)
        {
            var physicalActivities = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize);

            var physicalActivitiesDTO = new List<PhysicalActivityDTO>();
            if (physicalActivities.Count() > 0)
            {
                foreach (var physicalActivity in physicalActivities)
                {
                    physicalActivitiesDTO.Add(CreatePhysicalActivityDTO(physicalActivity)!);
                }
            }
            return physicalActivitiesDTO;
        }



        /// <summary>
        /// Создание модели DTO
        /// </summary>
        /// <param name="physicalActivity">Модель</param>
        /// <returns>Модель DTO</returns>
        private PhysicalActivityDTO? CreatePhysicalActivityDTO(PhysicalActivity? physicalActivity)
        {
            if (physicalActivity == null) return null;
            return new PhysicalActivityDTO
            {
               Id=physicalActivity.Id,
               EnergyEquivalent=physicalActivity.EnergyEquivalent,
               Name=physicalActivity.Name,
            };
        }


        /// <summary>
        /// Получить запись о физической автивности по ИД
        /// </summary>
        /// <param name="activityId">ИД физ. активности</param>
        /// <returns>Модель DTO</returns>
        public async Task<PhysicalActivityDTO?> GetPhysicalActivityByIdAsync(int activityId)
        {
            return CreatePhysicalActivityDTO(await _repository.GetByIdAsync(activityId));
        }



        /// <summary>
        /// Получить список физической акстивности по строке поиска.
        /// Разные фразы для поиска разделяются ","
        /// </summary>
        /// <param name="search">строка поиска. Для разделения фраз использовать ","</param>
        /// <returns>Список</returns>
        public async Task<IEnumerable<PhysicalActivity>> GetListPhysicalActivitiesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');

            var workrecords = await _repository.GetAllAsync();
            var filterrecords = new List<PhysicalActivity>();
            foreach (var item in stringsSearch) 
            {
                filterrecords.AddRange(workrecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList());                          
            }
            return filterrecords;
        }
    }
}
