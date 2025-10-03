using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с самочувствием(состоянием здоровья) пользователя
    /// </summary>
    /// <seealso cref="IHealthMetricValueService" />
    public class HealthConditionService(IHealthConditionRepository healthConditionRepository,
        ClaimsPrincipal authorization,
        IAccessToMetricsService accessToMetricsService) : IHealthConditionService
    {
        private readonly IHealthConditionRepository _repository = healthConditionRepository;
        private readonly ClaimsPrincipal _authorization = authorization;       
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;

        ///<inheritdoc/>
        public async Task CreateRecordOfHealthCondAsync(HealthCondition healthCondition)
        {
            if (!_authorization.IsInRole("Admin") && healthCondition.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthCondition.UserId,
                                                    _repository.Name);
            }

            await _repository.CreateAsync(healthCondition);
        }

        ///<inheritdoc/>
        public async Task DeleteRecordOfHealthCondAsync(int healthConditionId)
        {
            var healthCondFind = await _repository.GetByIdAsync(healthConditionId);

            if (healthCondFind == null)
            {
                throw new IncorrectOrEmptyResultException("Значение самочувствия пользователя не зарегистрировано",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(healthConditionId), healthConditionId }
                                                            });
            }

            if (!_authorization.IsInRole("Admin") && healthCondFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свои значения самочувствия",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthCondFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(healthConditionId);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<HealthCondition>> GetAllRecordsOfHealthCondByUserIdAsync(int userId, DateTime begDate, DateTime endDate)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && userId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(userId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о самочувствии",
                                                grantedUserId,
                                                userId,
                                                _repository.Name);
            }

            var recordsOfHealthCond = (await _repository.GetAllAsync())
                                        .Where(h => h.UserId == userId &&
                                                h.RecordedAt >= begDate &&
                                                h.RecordedAt <= endDate);

            return recordsOfHealthCond;
        }

        ///<inheritdoc/>
        public async Task<HealthCondition> GetRecordOfHealthCondByIdAsync(int healthConditionId)
        {
            var healthCondFind = await _repository.GetByIdAsync(healthConditionId);
            if (healthCondFind == null)
            {
                throw new IncorrectOrEmptyResultException("Значение самочувствия пользователя не зарегистрировано",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(healthConditionId), healthConditionId }
                                                            });
            }

            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && healthCondFind.UserId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(healthCondFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о самочувствии",
                                                grantedUserId,
                                                healthCondFind.UserId,
                                                _repository.Name);
            }

            return healthCondFind;
        }

        ///<inheritdoc/>
        public async Task UpdateRecordOfHealthCondAsync(HealthCondition healthCondition)
        {
            var healthCondFind = await _repository.GetByIdAsync(healthCondition.Id);

            if (healthCondFind == null)
            {
                throw new IncorrectOrEmptyResultException("Значение самочувствия пользователя не зарегистрировано",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(healthCondition), healthCondition}
                                                            });
            }

            if (!_authorization.IsInRole("Admin") && healthCondFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете изменять значения самочувствия для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthCondFind.UserId,
                                                    _repository.Name);
            }

            healthCondition.UserId = healthCondFind.UserId;

            await _repository.UpdateAsync(healthCondition);
        }
    }
}
