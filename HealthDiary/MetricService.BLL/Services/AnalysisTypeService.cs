using AutoMapper;
using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными справочника "Типы анализов"
    /// </summary>
    /// <seealso cref="IAnalysisTypeService" />
    public class AnalysisTypeService(IAnalysisTypeRepository repository, IValidator<AnalysisType> validator, ClaimsPrincipal authorizationService, IMapper mapper) : IAnalysisTypeService
    {
        private readonly IAnalysisTypeRepository _repository = repository;
        private readonly IValidator<AnalysisType> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task<IEnumerable<AnalysisTypeDTO>> GetAllAnalysisTypeAsync()
        {
            return _mapper.Map<IEnumerable<AnalysisTypeDTO>>(await _repository.GetAllAsync());           
        }


        /// <inheritdoc/>
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


        /// <inheritdoc/>
        public async Task<IEnumerable<AnalysisTypeDTO>> GetListAnalysisTypeBySearchAsync(string search)
        {
           return _mapper.Map<IEnumerable<AnalysisTypeDTO>>(await _repository.GetListAnalysisTypeBySearchAsync(search));
        }


        /// <inheritdoc/>
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


        /// <inheritdoc/>
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


        /// <inheritdoc/>
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

