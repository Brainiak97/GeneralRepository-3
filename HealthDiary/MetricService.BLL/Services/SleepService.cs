using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о сне пользователя
    /// </summary>
    /// <seealso cref="ISleepService" />
    public class SleepService(ISleepRepository sleepRepository, IValidator<Sleep> validator,
        ClaimsPrincipal authorization, IMapper mapper, IAccessToMetricsService accessToMetricsService) : ISleepService
    {
        private readonly ISleepRepository _repository = sleepRepository;
        private readonly IValidator<Sleep> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;


        /// <inheritdoc/>
        public async Task DeleteRecordOfSleepAsync(int sleepId)
        {
            var sleepFind = await _repository.GetByIdAsync(sleepId) ??
                throw new IncorrectOrEmptyResultException("Указанный пользователь не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            { nameof(sleepId), sleepId }
                                                        });

            if (!_authorization.IsInRole("Admin") && sleepFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись о сне",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    sleepFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(sleepId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<SleepDTO>> GetAllRecordsOfSleepByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(requestListWithPeriodByIdDTO.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о сне",
                                                    grantedUserId,
                                                    requestListWithPeriodByIdDTO.UserId,
                                                    _repository.Name);
            }

            var sleeps = (await _repository.GetAllAsync())
                            .Where(s => s.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    s.StartSleep >= requestListWithPeriodByIdDTO.BegDate &&
                                    s.StartSleep <= requestListWithPeriodByIdDTO.EndDate);
            return _mapper.Map<IEnumerable<SleepDTO>>(sleeps);
        }


        /// <inheritdoc/>
        public async Task<SleepDTO> GetRecordOfSleepByIdAsync(int sleepId)
        {
            var sleepFind = await _repository.GetByIdAsync(sleepId) ??
            throw new IncorrectOrEmptyResultException("Указанный пользователь не существует",
                                                    new Dictionary<object, object>()
                                                    {
                                                        { "sleepId", sleepId }
                                                    });

            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && sleepFind.UserId != grantedUserId &&
                                await _accessToMetricsService.CheckAccessToMetricsAsync(sleepFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о сне",
                                                   grantedUserId,
                                                    sleepFind.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<SleepDTO>(sleepFind);
        }


        /// <inheritdoc/>
        public async Task CreateRecordOfSleepAsync(SleepCreateDTO sleepCreateDTO)
        {
            if (!_authorization.IsInRole("Admin") && (sleepCreateDTO.UserId != Common.Common.GetAuthorId(_authorization)))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о сне для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    sleepCreateDTO.UserId,
                                                    _repository.Name);
            }

            var sleep = _mapper.Map<Sleep>(sleepCreateDTO);

            if (!_validator.Validate(sleep, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о сне пользователя", errorList);
            }

            await _repository.CreateAsync(sleep);
        }


        /// <inheritdoc/>
        public async Task UpdateRecordOfSleepAsync(SleepUpdateDTO sleepUpdateDTO)
        {
            var findSleep = await _repository.GetByIdAsync(sleepUpdateDTO.Id) ??
                           throw new IncorrectOrEmptyResultException("Сон не зарегистрирован",
                                                                       new Dictionary<object, object>()
                                                                       {
                                                                            {"sleepDTO", sleepUpdateDTO}
                                                                       });


            if (!_authorization.IsInRole("Admin") && (findSleep.UserId != Common.Common.GetAuthorId(_authorization)))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о сне для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    findSleep.UserId,
                                                    _repository.Name);
            }

            var sleep = _mapper.Map<Sleep>(sleepUpdateDTO);
            sleep.UserId = findSleep.UserId;

            if (!_validator.Validate(sleep, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о сне пользователя", errorList);
            }

            await _repository.UpdateAsync(sleep);
        }
    }
}
