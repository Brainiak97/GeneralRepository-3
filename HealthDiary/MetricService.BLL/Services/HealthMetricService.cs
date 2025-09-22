using AutoMapper;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о показателях здоровья пользователя
    /// </summary>
    /// <seealso cref="IHealthMetricService" />
    public class HealthMetricService(IHealthMetricRepository healthMetricRepository,
        IHealthMetricValueRepository healthMetricValueRepository,
        ClaimsPrincipal authorization, IMapper mapper) : IHealthMetricService
    {
        private readonly IHealthMetricRepository _healthMetricRepository = healthMetricRepository;
        private readonly IHealthMetricValueRepository _healthMetricValueRepository = healthMetricValueRepository;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task DeleteHealthMetricAsync(int healthMetricId)
        {
            var healthMetricFind = await _healthMetricRepository.GetByIdAsync(healthMetricId) ??
               throw new IncorrectOrEmptyResultException("Указанная запись о показателях здоровья пользователя не существует",
                                                       new Dictionary<object, object>()
                                                       {
                                                            { nameof(healthMetricId), healthMetricId }
                                                       });

            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам запрещено удалять показатели здоровья пользователя",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    healthMetricFind.Id,
                                                    _healthMetricRepository.Name);
            }

            await _healthMetricRepository.DeleteAsync(healthMetricId);
        }

        /// <inheritdoc/>
        public async Task<HealthMetric> GetHealthMetricByIdAsync(int healthMetricId)
        {
            var healthMetricFind = await _healthMetricRepository.GetByIdAsync(healthMetricId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись о показателях здоровья пользователя не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            { nameof(healthMetricId), healthMetricId }
                                                        });

            return healthMetricFind;
        }


        /// <inheritdoc/>
        public async Task UpdateHealthMetricAsync(HealthMetric healthMetric)
        {
            var healthMetricFind = await _healthMetricRepository.GetByIdAsync(healthMetric.Id) ??
                throw new IncorrectOrEmptyResultException("Запись о показателях здоровья пользователя не зарегистрирована",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(healthMetric), healthMetric}
                                                            });
            var existsLinks = (await _healthMetricValueRepository.GetByHealthMetricIdAsync(healthMetricFind.Id)).Any();
            if (existsLinks)
            {
                throw new InvalidOperationException("Показатель здоровья пользователя редактировать нельзя, есть ссылки");
            }

            await _healthMetricRepository.UpdateAsync(healthMetric);
        }

        /// <inheritdoc/>
        public async Task CreateHealthMetricAsync(HealthMetric healthMetric)
        {
            await _healthMetricRepository.CreateAsync(healthMetric);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HealthMetric>> GetAllHealthMetricsAsync()
        {
            return await _healthMetricRepository.GetAllAsync();
        }
    }
}
