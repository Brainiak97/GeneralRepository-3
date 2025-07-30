using AutoMapper;
using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными справочника "Категории анализов"
    /// </summary>
    /// <seealso cref="IAnalysisCategoryService" />
    public class AnalysisCategoryService(IAnalysisCategoryRepository repository, IValidator<AnalysisCategory> validator,
        ClaimsPrincipal authorization, IMapper mapper) : IAnalysisCategoryService
    {
        private readonly IAnalysisCategoryRepository _repository = repository;
        private readonly IValidator<AnalysisCategory> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<IEnumerable<AnalysisCategoryDTO>> GetAllAnalysisCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<AnalysisCategoryDTO>>(await _repository.GetAllAsync());
        }

        /// <inheritdoc/>
        public async Task<AnalysisCategoryDTO?> GetAnalysisCategoryByIdAsync(int categoryId)
        {
            var analysisCategory = await _repository.GetByIdAsync(categoryId);

            if (analysisCategory == null)
            {
                throw new IncorrectOrEmptyResultException("Указанная категория анализов не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            { nameof(categoryId), categoryId }
                                                        });
            }
            return _mapper.Map<AnalysisCategoryDTO>(analysisCategory);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AnalysisCategoryDTO>> GetListAnalysisCategoriesBySearchAsync(string search)
        {
            return _mapper.Map<IEnumerable<AnalysisCategoryDTO>>(await _repository.GetListAnalysisCategoriesBySearchAsync(search));
        }

        /// <inheritdoc/>
        public async Task CreateAnalysisCategoryAsync(AnalysisCategoryCreateDTO analysisCategoryCreateDTO)
        {
            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    0,
                                                    _repository.Name);
            }

            var analysisCategory = _mapper.Map<AnalysisCategory>(analysisCategoryCreateDTO);

            if (!_validator.Validate(analysisCategory, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о категории анализов", errorList);
            }

            await _repository.CreateAsync(analysisCategory);
        }

        /// <inheritdoc/>
        public async Task UpdateAnalysisCategoryAsync(AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(analysisCategoryUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Категория анализов не зарегистрирована",
                                                        new Dictionary<object, object>()
                                                        {
                                                            {nameof(analysisCategoryUpdateDTO), analysisCategoryUpdateDTO}
                                                        });

            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    0,
                                                    _repository.Name);
            }

            var analysisCategory = _mapper.Map<AnalysisCategory>(analysisCategoryUpdateDTO);

            if (!_validator.Validate(analysisCategory, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о категории анализов", errorList);
            }

            await _repository.UpdateAsync(analysisCategory);
        }

        /// <inheritdoc/>
        public async Task DeleteAnalysisCategoryAsync(int analysisCategoryId)
        {
            _ = await _repository.GetByIdAsync(analysisCategoryId) ??
               throw new IncorrectOrEmptyResultException("Категория анализов не зарегистрирована",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(analysisCategoryId), analysisCategoryId }
                                                           });

            if (!_authorization.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные",
                                                    Common.Common.GetAuthorId(_authorization),
                                                    0,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(analysisCategoryId);
        }
    }
}
