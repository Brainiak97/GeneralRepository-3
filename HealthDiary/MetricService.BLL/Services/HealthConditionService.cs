using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthCondition;
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
        IMapper mapper) : IHealthConditionService
    {
        private readonly IHealthConditionRepository _repository = healthConditionRepository;
        private readonly ClaimsPrincipal _authorization = authorization;        
        private readonly IMapper _mapper=mapper;

        ///<inheritdoc/>
        public async Task CreateHealthConditionAsync(HealthConditionCreateDTO healthConditionCreateDTO)
        {
            var healthCondition = _mapper.Map<HealthCondition>(healthConditionCreateDTO);
            Common.Common.CheckAccessAndThrow(_authorization, healthCondition.UserId, _repository.Name);

            await _repository.CreateAsync(healthCondition);
        }

        ///<inheritdoc/>
        public async Task DeleteHealthConditionAsync(int healthConditionId)
        {
            var healthCondFind = await GetHealthConditionByIdAsync(healthConditionId);

            await _repository.DeleteAsync(healthConditionId);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<HealthCondition>> GetAllHealthConditionsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            Common.Common.CheckAccessAndThrow(_authorization, requestListWithPeriodByIdDTO.UserId, _repository.Name);

            var recordsOfHealthCond = (
                await _repository.GetAllAsync())
                .Where(h => h.UserId == requestListWithPeriodByIdDTO.UserId
                && h.RecordedAt >= requestListWithPeriodByIdDTO.BegDate
                && h.RecordedAt <= requestListWithPeriodByIdDTO.EndDate);

            return recordsOfHealthCond;
        }

        ///<inheritdoc/>
        public async Task<HealthCondition> GetHealthConditionByIdAsync(int healthConditionId)
        {
            var healthCondFind = await _repository.GetByIdAsync(healthConditionId);
            if (healthCondFind == null)
            {
                throw new IncorrectOrEmptyResultException(
                    "Значение самочувствия пользователя не зарегистрировано",
                    new Dictionary<object, object>()
                    {
                        { nameof(healthConditionId), healthConditionId }
                    });
            }

            Common.Common.CheckAccessAndThrow(_authorization, healthCondFind.UserId, _repository.Name);

            return healthCondFind;
        }

        ///<inheritdoc/>
        public async Task UpdateHealthConditionAsync(HealthConditionUpdateDTO healthConditionUpdateDTO)
        {
            var healthCondFind = await GetHealthConditionByIdAsync(healthConditionUpdateDTO.Id);

            var healthCondition = _mapper.Map<HealthCondition>(healthConditionUpdateDTO);

            healthCondition.UserId = healthCondFind.UserId;

            await _repository.UpdateAsync(healthCondition);
        }
    }
}
