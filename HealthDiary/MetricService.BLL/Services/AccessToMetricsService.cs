using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AccessToMetrics;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными доступа к личным метрикам пользователя
    /// </summary>    
    /// <seealso cref="IAccessToMetricsService" />
    public class AccessToMetricsService(IAccessToMetricsRepository accessToMetricsRepository, ClaimsPrincipal authorizationService, IMapper mapper) : IAccessToMetricsService
    {
        private readonly IAccessToMetricsRepository _repository = accessToMetricsRepository;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task CreateAccessToMetricsAsync(AccessToMetricsCreateDTO accessToMetricsCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && accessToMetricsCreateDTO.ProviderUserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    accessToMetricsCreateDTO.ProviderUserId,
                                                    _repository.Name);
            }

            AccessToMetrics accessToMetrics = _mapper.Map<AccessToMetrics>(accessToMetricsCreateDTO);


            await _repository.CreateAsync(accessToMetrics);
        }


        /// <inheritdoc/>
        public async Task DeleteAccessToMetricsAsync(int accessToMetricsId)
        {
            var accessToMetricsFind = await _repository.GetByIdAsync(accessToMetricsId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись о доступе не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(accessToMetricsId), accessToMetricsId }
                                                            });

            if (!_authorizationService.IsInRole("Admin") &&
                    accessToMetricsFind.ProviderUserId != Common.Common.GetAuthorId(_authorizationService) &&
                    accessToMetricsFind.GrantedUserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только записи о доступе к личным метрикам, в которых вы поставщик ли получатель",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    accessToMetricsFind.ProviderUserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(accessToMetricsId);
        }


        /// <inheritdoc/>
        public async Task<AccessToMetricsDTO?> GetAccessToMetricsByIdAsync(int accessToMetricsId)
        {
            var accessToMetricsFind = await _repository.GetByIdAsync(accessToMetricsId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись о доступе не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(accessToMetricsId), accessToMetricsId }
                                                            });

            if (!_authorizationService.IsInRole("Admin") &&
                accessToMetricsFind.ProviderUserId != Common.Common.GetAuthorId(_authorizationService) &&
                accessToMetricsFind.GrantedUserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только записи о доступе к личным метрикам, в которых вы поставщик ли получатель",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    accessToMetricsFind.ProviderUserId,
                                                    _repository.Name);
            }

            return _mapper.Map<AccessToMetricsDTO>(accessToMetricsFind);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<AccessToMetricsDTO>> GetAllAccessToMetricsByGrantedUserIdAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO)
        {

            var accessToMetricsList = (await GetAllAccessToMetricsAsync(requestAccessListWithPeriodByIdDTO))
                .Where(a => a.GrantedUserId == requestAccessListWithPeriodByIdDTO.UserId);

            return _mapper.Map<IEnumerable<AccessToMetricsDTO>>(accessToMetricsList);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<AccessToMetricsDTO>> GetAllAccessToMetricsByProviderUserIdAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO)
        {
            var accessToMetricsList = (await GetAllAccessToMetricsAsync(requestAccessListWithPeriodByIdDTO))
                .Where(a => a.ProviderUserId == requestAccessListWithPeriodByIdDTO.UserId);

            return _mapper.Map<IEnumerable<AccessToMetricsDTO>>(accessToMetricsList);
        }

        private async Task<IEnumerable<AccessToMetrics>> GetAllAccessToMetricsAsync(RequestAccessListWithPeriodByIdDTO requestAccessListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && requestAccessListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только записи о доступе к личным метрикам, в которых вы поставщик ли получатель",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    requestAccessListWithPeriodByIdDTO.UserId,
                                                    _repository.Name);
            }

            IEnumerable<AccessToMetrics> accessToMetricsList;

            accessToMetricsList = await _repository.GetAllAsync();


            if (requestAccessListWithPeriodByIdDTO.AllRecords == false)
            {
                accessToMetricsList = accessToMetricsList.Where(a => a.IsPermanentAccess == true || a.AccessExpirationDate >= DateOnly.FromDateTime(DateTime.Now));
            }

            return accessToMetricsList;
        }


        /// <inheritdoc/>
        public async Task UpdateAccessToMetricsAsync(AccessToMetricsUpdateDTO accessToMetricsUpdateDTO)
        {
            var accessToMetricsFind = await _repository.GetByIdAsync(accessToMetricsUpdateDTO.Id) ??
                 throw new IncorrectOrEmptyResultException("Указанная запись о доступе не существует",
                                                             new Dictionary<object, object>()
                                                             {
                                                                {nameof(accessToMetricsUpdateDTO), accessToMetricsUpdateDTO}
                                                             });

            if (!_authorizationService.IsInRole("Admin") && accessToMetricsFind.ProviderUserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о доступе к личным метрикам другим пользователям",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    accessToMetricsFind.ProviderUserId,
                                                    _repository.Name);
            }

            var updateAccessToMetrics = _mapper.Map<AccessToMetrics>(accessToMetricsUpdateDTO);
            updateAccessToMetrics.ProviderUserId = accessToMetricsFind.ProviderUserId;

            await _repository.UpdateAsync(updateAccessToMetrics);
        }

        ///<inheritdoc/>
        public async Task<bool> CheckAccessToMetricsAsync(int providerUserId, int grantedUserId)
        {
            return await _repository.CheckAccessToMetricsAsync(providerUserId, grantedUserId);
        }
    }
}
