using AutoMapper;
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
        public async Task CreateHealthMetricValueAsync(HealthMetricValue healthMetricValue)
        {
            await _repository.CreateAsync(healthMetricValue);
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
        public async Task<IEnumerable<HealthMetricValue>> GetAllHealthMetricsValueByUserIdAsync(int userId, DateTime begDate, DateTime endDate)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && userId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(userId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья",
                                                grantedUserId,
                                                userId,
                                                _repository.Name);
            }

            var recordsOfHealthMetricValue = (await _repository.GetAllAsync())
                                        .Where(h => h.UserId == userId &&
                                                h.RecordedAt >= begDate &&
                                                h.RecordedAt <= endDate);

            return recordsOfHealthMetricValue;
        }

        /// <inheritdoc/>     
        public async Task<HealthMetricValue> GetHealthMetricValueByIdAsync(int healthMetricId)
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

            return _mapper.Map<HealthMetricValue>(healthMetricValueFind);
        }

        /// <inheritdoc/>     
        public async Task UpdateHealthMetricValueAsync(HealthMetricValue healthMetricValue)
        {
            var healthMetricValueFind = await _repository.GetByIdAsync(healthMetricValue.Id) ??
               throw new IncorrectOrEmptyResultException("Значение показателей здоровья пользователя не зарегистрировано",
                                                           new Dictionary<object, object>()
                                                           {
                                                                {nameof(healthMetricValue), healthMetricValue}
                                                           });

            if (!_authorization.IsInRole("Admin") && healthMetricValueFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете изменять значения показателей здоровья для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthMetricValueFind.UserId,
                                                    _repository.Name);
            }

            await _repository.UpdateAsync(healthMetricValue);
        }
    }
}
