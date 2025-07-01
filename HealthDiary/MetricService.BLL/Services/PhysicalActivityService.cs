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
       

        
        public async Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync(int pageNum, int pageSize)
        {
            var physicalActivities = (await _repository.GetAllAsync())
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToPhysicalActivityDTO();

            
            return physicalActivities;
        }

        
        public async Task<PhysicalActivityDTO> GetPhysicalActivityByIdAsync(int activityId)
        {                  
            return (await _repository.GetByIdAsync(activityId)??
                 throw new IncorrectOrEmptyResultException("Указанная физическая активность не существует", new Dictionary<object, object>()
               {
                   { nameof(activityId), activityId }
               })).ToPhysicalActivityDTO();
        }

        
        public async Task<IEnumerable<PhysicalActivityDTO>> GetListPhysicalActivitiesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');

            var workrecords = await _repository.GetAllAsync();
            var filterrecords = new List<PhysicalActivityDTO>();
            foreach (var item in stringsSearch) 
            {
                filterrecords.AddRange(workrecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    .ToList()
                    .ToPhysicalActivityDTO());                          
            }
           
            return filterrecords;
        }

        
        public async Task CreatePhysicalActivityAsync(PhysicalActivityCreateDTO physicalActivityCreateDTO)
        {           

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            PhysicalActivity physicalActivity = physicalActivityCreateDTO.ToPhysicalActivity();

            if (!_validator.Validate(physicalActivity, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о физической активности", errorList);
            }

            await _repository.CreateAsync(physicalActivity);
        }


        
        public async Task UpdatePhysicalActivityAsync(PhysicalActivityUpdateDTO physicalActivityUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(physicalActivityUpdateDTO.Id)??
                throw new IncorrectOrEmptyResultException("Физическая активность не зарегистрирована",
                    new Dictionary<object, object>()
                    {
                        {nameof(physicalActivityUpdateDTO), physicalActivityUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            var physicalActivityFind = physicalActivityUpdateDTO.ToPhysicalActivity();

            if (!_validator.Validate(physicalActivityFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о физической активности", errorList);
            }

            await _repository.UpdateAsync(physicalActivityFind);
        }

       
        public async Task DeletePhysicalActivityAsync(int physicalActivityId)
        {
            _ = await _repository.GetByIdAsync(physicalActivityId) ??
               throw new IncorrectOrEmptyResultException("Физическая активность не зарегистрирована", new Dictionary<object, object>()
               {
                    { nameof(physicalActivityId), physicalActivityId }
               });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            await _repository.DeleteAsync(physicalActivityId);
        }               
    }
}
