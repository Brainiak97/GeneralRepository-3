using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Regimen;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о схеме приема медикаментов пользователем
    /// </summary>
    /// <seealso cref="IRegimenService" />
    public class RegimenService(IRegimenRepository regimenRepository, IValidator<Regimen> validator,
        ClaimsPrincipal authorization, IMapper mapper, IAccessToMetricsService accessToMetricsService) : IRegimenService
    {
        private readonly IRegimenRepository _repository = regimenRepository;
        private readonly IValidator<Regimen> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;


        /// <inheritdoc/>
        public async Task CreateRegimenAsync(RegimenCreateDTO regimenCreateDTO)
        {
            if (!_authorization.IsInRole("Admin") && regimenCreateDTO.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    regimenCreateDTO.UserId,
                                                    _repository.Name);
            }

            var regimen = _mapper.Map<Regimen>(regimenCreateDTO);

            if (!_validator.Validate(regimen, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о схеме приема лекарств", errorList);
            }

            await _repository.CreateAsync(regimen);
        }


        /// <inheritdoc/>
        public async Task DeleteRegimenAsync(int regimenId)
        {
            var regimenFind = await _repository.GetByIdAsync(regimenId) ??
                 throw new IncorrectOrEmptyResultException("Указанная схема приема не существует",
                                                             new Dictionary<object, object>()
                                                             {
                                                                { nameof(regimenId), regimenId }
                                                             });

            if (!_authorization.IsInRole("Admin") && regimenFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою схему приема",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    regimenFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(regimenId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<RegimenDTO>> GetAllRegimenByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") &&
                requestListWithPeriodByIdDTO.UserId != grantedUserId &&
                await _accessToMetricsService.CheckAccessToMetricsAsync(requestListWithPeriodByIdDTO.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои схемы приема",
                    grantedUserId,
                    requestListWithPeriodByIdDTO.UserId,
                    _repository.Name);
            }

            var regimens = (await _repository.GetAllAsync())
                .Where(r => r.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    r.StartDate >= requestListWithPeriodByIdDTO.BegDate &&
                                    r.StartDate <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<RegimenDTO>>(regimens);
        }


        /// <inheritdoc/>
        public async Task<RegimenDTO> GetRegimenByIdAsync(int regimenId)
        {
            var regimenFind = await _repository.GetByIdAsync(regimenId) ??
                throw new IncorrectOrEmptyResultException("Указанная схема приема не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            {nameof(regimenId), regimenId }
                                                        });

            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") && regimenFind.UserId != grantedUserId &&
                        await _accessToMetricsService.CheckAccessToMetricsAsync(regimenFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку",
                                                    grantedUserId,
                                                    regimenFind.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<RegimenDTO>(regimenFind);
        }


        /// <inheritdoc/>
        public async Task UpdateRegimenAsync(RegimenUpdateDTO regimenUpdateDTO)
        {
            var regimenFind = await _repository.GetByIdAsync(regimenUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Схема приема не зарегистрирована",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(regimenUpdateDTO), regimenUpdateDTO}
                                                            });

            if (!_authorization.IsInRole("Admin") && regimenFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    regimenFind.UserId,
                                                    _repository.Name);
            }

            var regimen = _mapper.Map<Regimen>(regimenUpdateDTO);
            regimen.MedicationId = regimenFind.MedicationId;

            if (!_validator.Validate(regimen, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о схеме приема", errorList);
            }

            await _repository.UpdateAsync(regimen);
        }
    }
}
