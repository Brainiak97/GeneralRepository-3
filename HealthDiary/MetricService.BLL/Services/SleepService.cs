using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class SleepService(ISleepRepository sleepRepository, IValidator<Sleep> validator, ClaimsPrincipal authorizationService) : ISleepService
    {
        private readonly ISleepRepository _repository = sleepRepository;
        private readonly IValidator<Sleep> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;


        
        public async Task DeleteRecordOfSleepAsync(int sleepId)
        {
            var sleepFind = await _repository.GetByIdAsync(sleepId) ??
                throw new IncorrectOrEmptyResultException("Указанный пользователь не существует", new Dictionary<object, object>()
                {
                    { "sleepId", sleepId }
                });

            if (!_authorizationService.IsInRole("Admin") && sleepFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись о сне", 
                    Common.Common.GetAuthorId(_authorizationService), sleepFind.UserId, _repository.Name);
            }

            await _repository.DeleteAsync(sleepId);
        }

       
        public async Task<IEnumerable<SleepDTO>> GetAllRecordsOfSleepByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о сне", 
                    Common.Common.GetAuthorId(_authorizationService), requestListWithPeriodByIdDTO.UserId, _repository.Name);
            }

            var sleeps = (await _repository.GetAllAsync()).Where(s => s.UserId == requestListWithPeriodByIdDTO.UserId && s.StartSleep >= requestListWithPeriodByIdDTO.BegDate && 
            s.StartSleep <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize).Take(requestListWithPeriodByIdDTO.PageSize).ToSleepDTO();
           
            return sleeps;
        }


        
        public async Task<SleepDTO> GetRecordOfSleepByIdAsync(int sleepId)
        {
            var sleepFind = await _repository.GetByIdAsync(sleepId) ??
            throw new IncorrectOrEmptyResultException("Указанный пользователь не существует", new Dictionary<object, object>()
                {
                    { "sleepId", sleepId }
                });

            if (!_authorizationService.IsInRole("Admin") && sleepFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о сне", 
                    Common.Common.GetAuthorId(_authorizationService), sleepFind.UserId, _repository.Name);
            }

            return sleepFind.ToSleepDTO();
        }

         
        public async Task CreateRecordOfSleepAsync(SleepCreateDTO sleepDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && (sleepDTO.UserId != Common.Common.GetAuthorId(_authorizationService)))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о сне для других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService), sleepDTO.UserId, _repository.Name);
            }

            Sleep sleep = sleepDTO.ToSleep();

            if (!_validator.Validate(sleep, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о сне пользователя", errorList);
            }

            await _repository.CreateAsync(sleep);
        }

        
        public async Task UpdateRecordOfSleepAsync(SleepUpdateDTO sleepDTO)
        {
            var findSleep = await _repository.GetByIdAsync(sleepDTO.Id) ??
                           throw new IncorrectOrEmptyResultException("Сон не зарегистрирован",
                               new Dictionary<object, object>()
                               {
                                    {"sleepDTO", sleepDTO}
                               });


            if (!_authorizationService.IsInRole("Admin") && (findSleep.UserId != Common.Common.GetAuthorId(_authorizationService)))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о сне для других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService), findSleep.UserId, _repository.Name);
            }

            findSleep = sleepDTO.ToSleep(findSleep.UserId);

            if (!_validator.Validate(findSleep, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о сне пользователя", errorList);
            }

            await _repository.UpdateAsync(findSleep);
        }                
    }
}
