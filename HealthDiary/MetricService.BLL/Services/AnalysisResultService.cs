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
    public class AnalysisResultService(IAnalysisResultRepository analysisResultRepository, IValidator<AnalysisResult> validator, ClaimsPrincipal authorizationService, IMapper mapper) : IAnalysisResultService
    {
        private readonly IAnalysisResultRepository _repository = analysisResultRepository;
        private readonly IValidator<AnalysisResult> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;



        public async Task CreateAnalysisResultAsync(AnalysisResultCreateDTO analysisResultCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && (analysisResultCreateDTO.UserId != Common.Common.GetAuthorId(_authorizationService)))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
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


        public async Task DeleteAnalysisResultAsync(int analysisResultId)
        {
            var analysisFind = await _repository.GetByIdAsync(analysisResultId) ??
               throw new IncorrectOrEmptyResultException("Указанная запись о результате анализа пользователя не существует",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(analysisResultId), analysisResultId }
                                                           });


            if (!_authorizationService.IsInRole("Admin") && analysisFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалять только свои записи",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    analysisFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(analysisResultId);
        }


        public async Task<IEnumerable<AnalysisResultDTO>> GetAllAnalysisResultsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои анализы",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    requestListWithPeriodByIdDTO.UserId,
                                                    _repository.Name);
            }

            var analysisResults = (await _repository.GetAllAsync()).Where(a => a.UserId == requestListWithPeriodByIdDTO.UserId &&
                                                                            a.TestedAt >= requestListWithPeriodByIdDTO.BegDate &&
                                                                            a.TestedAt <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize)
                .Take(requestListWithPeriodByIdDTO.PageSize);

            return _mapper.Map<IEnumerable<AnalysisResultDTO>>(analysisResults);
        }


        public async Task<AnalysisResultDTO?> GetAnalysisResultByIdAsync(int analysisResultId)
        {
            var analysisResultFind = await _repository.GetByIdAsync(analysisResultId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись об анализах не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(analysisResultId), analysisResultId }
                                                            });

            if (!_authorizationService.IsInRole("Admin") && analysisResultFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    analysisResultFind.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<AnalysisResultDTO>(analysisResultFind);
        }


        public async Task UpdateAnalysisResultAsync(AnalysisResultUpdateDTO analysisResultUpdateDTO)
        {
            var analysisResultFind = await _repository.GetByIdAsync(analysisResultUpdateDTO.Id) ??
               throw new IncorrectOrEmptyResultException("Запись об анализах не зарегистрирована",
                                                           new Dictionary<object, object>()
                                                           {
                                                                {nameof(analysisResultUpdateDTO), analysisResultUpdateDTO}
                                                           });

            if (!_authorizationService.IsInRole("Admin") && analysisResultFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    analysisResultFind.UserId,
                                                    _repository.Name);
            }

            var analysisResult=_mapper.Map<AnalysisResult>(analysisResultUpdateDTO);
            analysisResult.UserId=analysisResultFind.UserId;            

            if (!_validator.Validate(analysisResult, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные об анализах пользователя", errorList);
            }

            await _repository.UpdateAsync(analysisResult);
        }
    }
}
