using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о базовых медицинских показателях пользователя
    /// </summary>
    /// <seealso cref="IHealthMetricsBaseService" />
    public class HealthMetricsBaseService(IHealthMetricsBaseRepository healthMetricsBaseRepository,
        IValidator<HealthMetricsBase> validator, ClaimsPrincipal authorization, IMapper mapper,
        IAccessToMetricsService accessToMetricsService) : IHealthMetricsBaseService
    {
        private readonly IHealthMetricsBaseRepository _repository = healthMetricsBaseRepository;
        private readonly IValidator<HealthMetricsBase> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;


        /// <inheritdoc/>
        public async Task DeleteRecordOfHealthMetricsBaseAsync(int healthMetricsBaseId)
        {
            var healthMetricsBaseFind = await _repository.GetByIdAsync(healthMetricsBaseId) ??
               throw new IncorrectOrEmptyResultException("Указанная запись о базовых мед. показателях не существует",
                                                       new Dictionary<object, object>()
                                                       {
                                                            { nameof(healthMetricsBaseId), healthMetricsBaseId }
                                                       });

            if (!_authorization.IsInRole("Admin") && healthMetricsBaseFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись о базовых показателях здоровья",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthMetricsBaseFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(healthMetricsBaseId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<HealthMetricsBaseDTO>> GetAllRecordsOfHealthMetricsBaseByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(requestListWithPeriodByIdDTO.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья",
                                                grantedUserId,
                                                requestListWithPeriodByIdDTO.UserId,
                                                _repository.Name);
            }

            var recordsOfHealthMetricsBase = (await _repository.GetAllAsync())
                                        .Where(h => h.UserId == requestListWithPeriodByIdDTO.UserId &&
                                                h.MetricDate >= requestListWithPeriodByIdDTO.BegDate &&
                                                h.MetricDate <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<HealthMetricsBaseDTO>>(recordsOfHealthMetricsBase);
        }


        /// <inheritdoc/>
        public async Task<HealthMetricsBaseDTO> GetRecordOfHealthMetricsBaseByIdAsync(int healthMetricsBaseId)
        {
            var healthMetricsBaseFind = await _repository.GetByIdAsync(healthMetricsBaseId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись о базовых мед. показателях не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            { nameof(healthMetricsBaseId), healthMetricsBaseId }
                                                        });

            var grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && healthMetricsBaseFind.UserId != grantedUserId &&
                                    await _accessToMetricsService.CheckAccessToMetricsAsync(healthMetricsBaseFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья",
                                                    grantedUserId,
                                                    healthMetricsBaseFind.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<HealthMetricsBaseDTO>(healthMetricsBaseFind);
        }


        /// <inheritdoc/>
        public async Task UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseUpdateDTO healthMetricsBaseUpdateDTO)
        {
            var findHealthMetricsBase = await _repository.GetByIdAsync(healthMetricsBaseUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Запись о базовых мед. показателях не зарегистрирована",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(healthMetricsBaseUpdateDTO), healthMetricsBaseUpdateDTO}
                                                            });

            if (!_authorization.IsInRole("Admin") && findHealthMetricsBase.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о базовых показателях здоровья для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    findHealthMetricsBase.UserId,
                                                    _repository.Name);
            }

            var healthMetricsBase = _mapper.Map<HealthMetricsBase>(healthMetricsBaseUpdateDTO);
            healthMetricsBase.UserId = findHealthMetricsBase.UserId;

            if (!_validator.Validate(healthMetricsBase, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о базовых показателях здоровья пользователя", errorList);
            }

            await _repository.UpdateAsync(healthMetricsBase);
        }


        /// <inheritdoc/>
        public async Task CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseCreateDTO healthMetricsBaseCreateDTO)
        {

            if (!_authorization.IsInRole("Admin") && healthMetricsBaseCreateDTO.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о базовых показателях здоровья для других пользователей",
                                                Common.Common.GetAuthorId(_authorization),
                                                healthMetricsBaseCreateDTO.UserId,
                                                _repository.Name);
            }

            var healthMetricsBase = _mapper.Map<HealthMetricsBase>(healthMetricsBaseCreateDTO);

            if (!_validator.Validate(healthMetricsBase, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о базовых показателях здоровья пользователя", errorList);
            }

            await _repository.CreateAsync(healthMetricsBase);
        }
    }
}
