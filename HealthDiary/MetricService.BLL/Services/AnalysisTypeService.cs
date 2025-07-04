using AutoMapper;
using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class AnalysisTypeService(IAnalysisTypeRepository repository, IValidator<AnalysisType> validator, ClaimsPrincipal authorizationService, IMapper mapper) : IAnalysisTypeService
    {
        private readonly IAnalysisTypeRepository _repository = repository;
        private readonly IValidator<AnalysisType> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<AnalysisTypeDTO>> GetAllAnalysisTypeAsync()
        {
            return _mapper.Map<IEnumerable<AnalysisTypeDTO>>(await _repository.GetAllAsync());           
        }

        public async Task<AnalysisTypeDTO?> GetAnalysisTypeByIdAsync(int typeId)
        {
            var analysisType = await _repository.GetByIdAsync(typeId) ??
               throw new IncorrectOrEmptyResultException("Указанный тип анализов не существует",
                                                         new Dictionary<object, object>()
                                                         {
                                                               { nameof(typeId), typeId }
                                                         });

            return _mapper.Map<AnalysisTypeDTO>(analysisType);
        }


        public async Task<IEnumerable<AnalysisTypeDTO>> GetListAnalysisTypeBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');
            var workRecords = await _repository.GetAllAsync();
            var filterRecords = new List<AnalysisTypeDTO>();
            IEnumerable<AnalysisType> tempRecords;
            foreach (var item in stringsSearch)
            {
                tempRecords = workRecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
                filterRecords.AddRange(_mapper.Map<IEnumerable<AnalysisTypeDTO>>(tempRecords));
            }

            return filterRecords;
        }


        public async Task CreateAnalysisTypeAsync(AnalysisTypeCreateDTO analysisTypeCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            var analysisType = _mapper.Map<AnalysisType>(analysisTypeCreateDTO);

            if (!_validator.Validate(analysisType, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о типе анализов", errorList);
            }

            await _repository.CreateAsync(analysisType);
        }


        public async Task UpdateAnalysisTypeAsync(AnalysisTypeUpdateDTO analysisTypeUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(analysisTypeUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Тип анализов не зарегистрирован",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(analysisTypeUpdateDTO), analysisTypeUpdateDTO}
                                                            });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            var analysisType = _mapper.Map<AnalysisType>(analysisTypeUpdateDTO);

            if (!_validator.Validate(analysisType, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о типе анализа", errorList);
            }

            await _repository.UpdateAsync(analysisType);
        }


        public async Task DeleteAnalysisTypeAsync(int analysisTypeId)
        {
            _ = await _repository.GetByIdAsync(analysisTypeId) ??
               throw new IncorrectOrEmptyResultException("Тип анализов не зарегистрирован", 
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(analysisTypeId), analysisTypeId }
                                                           });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные", 
                                                    Common.Common.GetAuthorId(_authorizationService), 
                                                    0, 
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(analysisTypeId);
        }
    }
}

