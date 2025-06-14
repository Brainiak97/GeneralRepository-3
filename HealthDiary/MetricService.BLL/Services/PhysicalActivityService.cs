using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public  class PhysicalActivityService(IPhysicalActivityRepository physicalActivityRepository, IValidator<PhysicalActivity> validator, ClaimsPrincipal authorizationService) : IPhysicalActivityService
    {
        private readonly IPhysicalActivityRepository _repository= physicalActivityRepository;
        private readonly IValidator<PhysicalActivity> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
       

        /// <summary>
        /// Получить все записи о физической активности
        /// </summary>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Кол-во строк на странице для пагинации</param>
        /// <returns></returns>
        public async Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync(int pageNum, int pageSize)
        {
            var physicalActivities = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize).ToPhysicalActivityDTO();

            
            return physicalActivities;
        }

        /// <summary>
        /// Получить запись о физической автивности по ИД
        /// </summary>
        /// <param name="activityId">ИД физ. активности</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная физическая активность не существует</exception>
        public async Task<PhysicalActivityDTO> GetPhysicalActivityByIdAsync(int activityId)
        {                  
            return (await _repository.GetByIdAsync(activityId)??
                 throw new IncorrectOrEmptyResultException("Указанная физическая активность не существует", new Dictionary<object, object>()
               {
                   { "activityId", activityId }
               })).ToPhysicalActivityDTO();
        }

        /// <summary>
        /// Получить список физической акстивности по строке поиска.
        /// Разные фразы для поиска разделяются ","
        /// </summary>
        /// <param name="search">строка поиска. Для разделения фраз использовать ","</param>
        /// <returns>Список</returns>
        public async Task<IEnumerable<PhysicalActivityDTO>> GetListPhysicalActivitiesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');

            var workrecords = await _repository.GetAllAsync();
            var filterrecords = new List<PhysicalActivityDTO>();
            foreach (var item in stringsSearch) 
            {
                filterrecords.AddRange(workrecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList().ToPhysicalActivityDTO());                          
            }
           
            return filterrecords;
        }

        /// <summary>
        /// Создание данных о физической активности
        /// </summary>
        /// <param name="physicalActivityCreateDTO"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Физическая активность уже зарегистрирована</exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task CreatePhysicalActivityAsync(PhysicalActivityCreateDTO physicalActivityCreateDTO)
        {           

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            PhysicalActivity physicalActivity = physicalActivityCreateDTO.ToPhysicalActivity();

            if (!_validator.Validate(physicalActivity, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о физической активности", errorList);
            }

            await _repository.CreateAsync(physicalActivity);
        }


        /// <summary>
        /// Обновление данных о физической активности
        /// </summary>
        /// <param name="physicalActivityUpdateDTO"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException"></exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task UpdatePhysicalActivityAsync(PhysicalActivityUpdateDTO physicalActivityUpdateDTO)
        {
            var physicalActivityFind = await _repository.GetByIdAsync(physicalActivityUpdateDTO.Id)??
                throw new IncorrectOrEmptyResultException("Физическая активность не зарегистрирована",
                    new Dictionary<object, object>()
                    {
                        {"physicalActivityUpdateDTO", physicalActivityUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            physicalActivityFind = physicalActivityUpdateDTO.ToPhysicalActivity();

            if (!_validator.Validate(physicalActivityFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о физической активности", errorList);
            }

            await _repository.UpdateAsync(physicalActivityFind);
        }

        /// <summary>
        /// Удаление данных о физической активности
        /// </summary>
        /// <param name="physicalActivityId"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectOrEmptyResultException">Физическая активность не зарегистрирована</exception>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа</exception>
        public async Task DeletePhysicalActivityAsync(int physicalActivityId)
        {
            var physicalActivityFind = await _repository.GetByIdAsync(physicalActivityId) ??
               throw new IncorrectOrEmptyResultException("Физическая активность не зарегистрирована", new Dictionary<object, object>()
               {
                    { "physicalActivityId", physicalActivityId }
               });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            await _repository.DeleteAsync(physicalActivityId);
        }               
    }
}
