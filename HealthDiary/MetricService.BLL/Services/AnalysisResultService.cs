using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о результате анализа пользователя
    /// </summary>
    /// <seealso cref="IAnalysisResultService" />
    public class AnalysisResultService(IAnalysisResultRepository analysisResultRepository, IValidator<AnalysisResult> validator,
        ClaimsPrincipal authorization, IMapper mapper, IAccessToMetricsService accessToMetricsService) : IAnalysisResultService
    {
        private readonly IAnalysisResultRepository _repository = analysisResultRepository;
        private readonly IValidator<AnalysisResult> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;


        /// <inheritdoc/>
        public async Task CreateAnalysisResultAsync(AnalysisResultCreateDTO analysisResultCreateDTO)
        {
            if (!_authorization.IsInRole("Admin") && (analysisResultCreateDTO.UserId != Common.Common.GetAuthorId(_authorization)))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    analysisResultCreateDTO.UserId,
                                                    _repository.Name);
            }

            var analysisResult = _mapper.Map<AnalysisResult>(analysisResultCreateDTO);

            if (!_validator.Validate(analysisResult, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о результате анализа пользователя", errorList);
            }

            await _repository.CreateAsync(analysisResult);
        }


        /// <inheritdoc/>        
        public async Task DeleteAnalysisResultAsync(int analysisResultId)
        {
            var analysisFind = await _repository.GetByIdAsync(analysisResultId) ??
               throw new IncorrectOrEmptyResultException("Указанная запись о результате анализа пользователя не существует",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(analysisResultId), analysisResultId }
                                                           });


            if (!_authorization.IsInRole("Admin") && analysisFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вам разрешено удалять только свои записи",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    analysisFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(analysisResultId);
        }

        /// <inheritdoc/>

        public async Task<IEnumerable<AnalysisResultDTO>> GetAllAnalysisResultsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorization);

            if (!_authorization.IsInRole("Admin") &&
                                    requestListWithPeriodByIdDTO.UserId != grantedUserId &&
                                    await _accessToMetricsService.CheckAccessToMetricsAsync(requestListWithPeriodByIdDTO.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои анализы",
                                                    grantedUserId,
                                                    requestListWithPeriodByIdDTO.UserId,
                                                    _repository.Name);
            }

            var analysisResults = (await _repository.GetAllAsync())
                                    .Where(a => a.UserId == requestListWithPeriodByIdDTO.UserId &&
                                            a.TestedAt >= requestListWithPeriodByIdDTO.BegDate &&
                                            a.TestedAt <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<AnalysisResultDTO>>(analysisResults);
        }


        /// <inheritdoc/>    
        public async Task<AnalysisResultDTO?> GetAnalysisResultByIdAsync(int analysisResultId)
        {
            var analysisResultFind = await _repository.GetByIdAsync(analysisResultId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись об анализах не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(analysisResultId), analysisResultId }
                                                            });
            var grantedUserId = Common.Common.GetAuthorId(_authorization);
            if (!_authorization.IsInRole("Admin") && analysisResultFind.UserId != grantedUserId &&
                                await _accessToMetricsService.CheckAccessToMetricsAsync(analysisResultFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои данные",
                                                    grantedUserId,
                                                    analysisResultFind.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<AnalysisResultDTO>(analysisResultFind);
        }


        /// <inheritdoc/>
        public async Task UpdateAnalysisResultAsync(AnalysisResultUpdateDTO analysisResultUpdateDTO)
        {
            var analysisResultFind = await _repository.GetByIdAsync(analysisResultUpdateDTO.Id) ??
               throw new IncorrectOrEmptyResultException("Запись об анализах не зарегистрирована",
                                                           new Dictionary<object, object>()
                                                           {
                                                                {nameof(analysisResultUpdateDTO), analysisResultUpdateDTO}
                                                           });

            if (!_authorization.IsInRole("Admin") && analysisResultFind.UserId != Common.Common.GetAuthorId(_authorization))
            {
                throw new ViolationAccessException("Вы не можете изменять данные других пользователей",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    analysisResultFind.UserId,
                                                    _repository.Name);
            }

            var analysisResult = _mapper.Map<AnalysisResult>(analysisResultUpdateDTO);
            analysisResult.UserId = analysisResultFind.UserId;

            if (!_validator.Validate(analysisResult, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные об анализах пользователя", errorList);
            }

            await _repository.UpdateAsync(analysisResult);
        }
    }
}
