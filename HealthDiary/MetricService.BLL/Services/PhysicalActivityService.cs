using AutoMapper;
using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными справочника "Физическая активность"
    /// </summary>
    /// <seealso cref="IPhysicalActivityService" />
    public class PhysicalActivityService(IPhysicalActivityRepository physicalActivityRepository, IValidator<PhysicalActivity> validator, ClaimsPrincipal authorization, IMapper mapper) : IPhysicalActivityService
    {
        private readonly IPhysicalActivityRepository _repository = physicalActivityRepository;
        private readonly IValidator<PhysicalActivity> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivitiesAsync()
        {
            return _mapper.Map<IEnumerable<PhysicalActivityDTO>>(await _repository.GetAllAsync());
        }


        /// <inheritdoc/>
        public async Task<PhysicalActivityDTO> GetPhysicalActivityByIdAsync(int activityId)
        {
            var physicalActivity = await _repository.GetByIdAsync(activityId) ??
                throw new IncorrectOrEmptyResultException("Указанная физическая активность не существует",
                                                            new Dictionary<object, object>()
                                                              {
                                                                   { nameof(activityId), activityId }
                                                              });
            return _mapper.Map<PhysicalActivityDTO>(physicalActivity);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<PhysicalActivityDTO>> GetListPhysicalActivitiesBySearchAsync(string search)
        {
            return _mapper.Map<IEnumerable<PhysicalActivityDTO>>(await _repository.GetListPhysicalActivitiesBySearchAsync(search));
        }


        /// <inheritdoc/>
        public async Task CreatePhysicalActivityAsync(PhysicalActivityCreateDTO physicalActivityCreateDTO)
        {
            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    0,
                                                    _repository.Name);
            }

            var physicalActivity = _mapper.Map<PhysicalActivity>(physicalActivityCreateDTO);

            if (!_validator.Validate(physicalActivity, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о физической активности", errorList);
            }

            await _repository.CreateAsync(physicalActivity);
        }


        /// <inheritdoc/>
        public async Task UpdatePhysicalActivityAsync(PhysicalActivityUpdateDTO physicalActivityUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(physicalActivityUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Физическая активность не зарегистрирована",
                                                        new Dictionary<object, object>()
                                                        {
                                                            {nameof(physicalActivityUpdateDTO), physicalActivityUpdateDTO}
                                                        });

            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    0,
                                                    _repository.Name);
            }

            var physicalActivity = _mapper.Map<PhysicalActivity>(physicalActivityUpdateDTO);

            if (!_validator.Validate(physicalActivity, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о физической активности", errorList);
            }

            await _repository.UpdateAsync(physicalActivity);
        }


        /// <inheritdoc/>
        public async Task DeletePhysicalActivityAsync(int physicalActivityId)
        {
            _ = await _repository.GetByIdAsync(physicalActivityId) ??
               throw new IncorrectOrEmptyResultException("Физическая активность не зарегистрирована",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(physicalActivityId), physicalActivityId }
                                                           });

            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    0,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(physicalActivityId);
        }
    }
}
