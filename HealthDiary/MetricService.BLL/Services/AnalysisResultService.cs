using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class AnalysisResultService(IAnalysisResultRepository analysisResultRepository, IValidator<AnalysisResult> validator, ClaimsPrincipal authorizationService) : IAnalysisResultService
    {
        private readonly IAnalysisResultRepository _repository = analysisResultRepository;
        private readonly IValidator<AnalysisResult> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;



        public async Task CreateAnalysisResultAsync(AnalysisResultCreateDTO analysisResultDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && (analysisResultDTO.UserId != Common.Common.GetAuthorId(_authorizationService)))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                    Common.Common.GetAuthorId(_authorizationService),
                    analysisResultDTO.UserId,
                    _repository.Name);
            }

            AnalysisResult analysisResult = analysisResultDTO.ToAnalysisResult();

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
                    Common.Common.GetAuthorId(_authorizationService), requestListWithPeriodByIdDTO.UserId, _repository.Name);
            }

            var analysisResults = (await _repository.GetAllAsync()).Where(a => a.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    a.TestedAt >= requestListWithPeriodByIdDTO.BegDate && a.TestedAt <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize)
                .Take(requestListWithPeriodByIdDTO.PageSize)
                .ToAnalysisResultDTO();

            return analysisResults;
        }


        public async Task<AnalysisResultDTO?> GetAnalysisResultByIdAsync(int analysisResultId)
        {
            var analysisResultFind = await _repository.GetByIdAsync(analysisResultId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись об анализах не существует", new Dictionary<object, object>()
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

            return analysisResultFind.ToAnalysisReaultDTO();
        }


        public async Task UpdateAnalysisResultAsync(AnalysisResultUpdateDTO analysisResultDTO)
        {
            var analysisResultFind = await _repository.GetByIdAsync(analysisResultDTO.Id) ??
               throw new IncorrectOrEmptyResultException("Запись об анализах не зарегистрирована",
                   new Dictionary<object, object>()
                   {
                        {nameof(analysisResultDTO), analysisResultDTO}
                   });

            if (!_authorizationService.IsInRole("Admin") && analysisResultFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    analysisResultFind.UserId, 
                    _repository.Name);
            }

            analysisResultFind = analysisResultDTO.ToAnalysisResult(analysisResultFind.UserId);

            if (!_validator.Validate(analysisResultFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные об анализах пользователя", errorList);
            }

            await _repository.UpdateAsync(analysisResultFind);
        }
    }
}
