using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetric;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с значениями показателей здоровья пользователя
    /// </summary>
    /// <seealso cref="IHealthMetricValueService" />
    public class HealthMetricValueService(IHealthMetricValueRepository healthMetricValueRepository,
        ClaimsPrincipal authorization, IMapper mapper,
        IAccessToMetricsService accessToMetricsService) : IHealthMetricValueService
    {
        private readonly IHealthMetricValueRepository _repository = healthMetricValueRepository;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;

        /// <inheritdoc/>       
        public async Task CreateHealthMetricValueAsync(HealthMetricValueCreateDTO healthMetricValueCreateDTO)
        {
            await _repository.CreateAsync(_mapper.Map<HealthMetricValue>(healthMetricValueCreateDTO));
        }

        /// <inheritdoc/>     
        public async Task DeleteHealthMetricValueAsync(int healthMetricValueId)
        {
            var healthMetricValueFind = await _repository.GetByIdAsync(healthMetricValueId) ??
               throw new IncorrectOrEmptyResultException("Значение показателей здоровья пользователя не зарегистрировано",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(healthMetricValueId), healthMetricValueId }
                                                           });
            if (!_authorization.IsInRole("Admin") && healthMetricValueFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свои значения показателей здоровья",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthMetricValueFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(healthMetricValueId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HealthMetricValueDTO>> GetAllHealthMetricsValueByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
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

            var recordsOfHealthMetricValue = (await _repository.GetAllAsync())
                                        .Where(h => h.UserId == requestListWithPeriodByIdDTO.UserId &&
                                                h.RecordedAt >= requestListWithPeriodByIdDTO.BegDate &&
                                                h.RecordedAt <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<HealthMetricValueDTO>>(recordsOfHealthMetricValue);
        }

        /// <inheritdoc/>     
        public async Task<HealthMetricValueDTO> GetHealthMetricValueByIdAsync(int healthMetricId)
        {
            var healthMetricValueFind = await _repository.GetByIdAsync(healthMetricId) ??
               throw new IncorrectOrEmptyResultException("Значение показателей здоровья пользователя не зарегистрировано",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(healthMetricId), healthMetricId }
                                                           });

            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && healthMetricValueFind.UserId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(healthMetricValueFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи показателей здоровья",
                                                grantedUserId,
                                                healthMetricValueFind.UserId,
                                                _repository.Name);
            }

            return _mapper.Map<HealthMetricValueDTO>(healthMetricValueFind);
        }

        /// <inheritdoc/>     
        public async Task UpdateHealthMetricValueAsync(HealthMetricValueUpdateDTO healthMetricValueUpdateDTO)
        {
            var healthMetricValueFind = await _repository.GetByIdAsync(healthMetricValueUpdateDTO.Id) ??
               throw new IncorrectOrEmptyResultException("Значение показателей здоровья пользователя не зарегистрировано",
                                                           new Dictionary<object, object>()
                                                           {
                                                                {nameof(healthMetricValueUpdateDTO), healthMetricValueUpdateDTO}
                                                           });

            if (!_authorization.IsInRole("Admin") && healthMetricValueFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете изменять значения показателей здоровья для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthMetricValueFind.UserId,
                                                    _repository.Name);
            }

            var updateHealthMetricValue = _mapper.Map<HealthMetricValue>(healthMetricValueUpdateDTO);
            updateHealthMetricValue.UserId = healthMetricValueFind.UserId;

            await _repository.UpdateAsync(updateHealthMetricValue);
        }
    }
}
