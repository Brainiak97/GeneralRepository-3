using AutoMapper;
using MetricService.BLL.DTO.HealthMetric;
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
        public async Task<HealthMetricDTO> GetHealthMetricByIdAsync(int healthMetricId)
        {
            var healthMetricFind = await _healthMetricRepository.GetByIdAsync(healthMetricId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись о показателях здоровья пользователя не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            { nameof(healthMetricId), healthMetricId }
                                                        });

            return _mapper.Map<HealthMetricDTO>(healthMetricFind);
        }


        /// <inheritdoc/>
        public async Task UpdateHealthMetricAsync(HealthMetricUpdateDTO healthMetricUpdateDTO)
        {
            var healthMetricFind = await _healthMetricRepository.GetByIdAsync(healthMetricUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Запись о показателях здоровья пользователя не зарегистрирована",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(healthMetricUpdateDTO), healthMetricUpdateDTO}
                                                            });
            var countLinks = (await _healthMetricValueRepository.GetListHealthMetricValueByHealthMetricIdAsync(healthMetricFind.Id)).Count();
            if (countLinks > 0)
            {
                throw new ReferenceToEntryException("Показатель здоровья пользователя редактировать нельзя, есть ссылки", healthMetricFind.Id, countLinks);
            }
            var healthMetric = _mapper.Map<HealthMetric>(healthMetricUpdateDTO);

            await _healthMetricRepository.UpdateAsync(healthMetric);
        }

        /// <inheritdoc/>
        public async Task CreateHealthMetricAsync(HealthMetricCreateDTO healthMetricCreateDTO)
        {
            await _healthMetricRepository.CreateAsync(_mapper.Map<HealthMetric>(healthMetricCreateDTO));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HealthMetricDTO>> GetAllHealthMetricsAsync()
        {
            return _mapper.Map<IEnumerable<HealthMetricDTO>>(await _healthMetricRepository.GetAllAsync());
        }
    }
}
