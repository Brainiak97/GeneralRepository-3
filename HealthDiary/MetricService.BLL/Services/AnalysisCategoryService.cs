using AutoMapper;
using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class AnalysisCategoryService(IAnalysisCategoryRepository repository, IValidator<AnalysisCategory> validator, ClaimsPrincipal authorizationService, IMapper mapper) : IAnalysisCategoryService
    {
        private readonly IAnalysisCategoryRepository _repository = repository;
        private readonly IValidator<AnalysisCategory> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;


        public async Task<IEnumerable<AnalysisCategoryDTO>> GetAllAnalysisCategoriesAsync(int pageNum, int pageSize)
        {
            var analysisCategories = (await _repository.GetAllAsync())
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize);

            return _mapper.Map<IEnumerable<AnalysisCategoryDTO>>(analysisCategories);
        }


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


        public async Task<IEnumerable<AnalysisCategoryDTO>> GetListAnalysisCategoriesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');
            var workRecords = await _repository.GetAllAsync();
            var filterRecords = new List<AnalysisCategoryDTO>();
            IEnumerable<AnalysisCategory> tempRecords;
            foreach (var item in stringsSearch)
            {
                tempRecords = workRecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase));
                filterRecords.AddRange(_mapper.Map<IEnumerable<AnalysisCategoryDTO>>(tempRecords));
            }

            return filterRecords;
        }


        public async Task CreateAnalysisCategoryAsync(AnalysisCategoryCreateDTO analysisCategoryCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
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


        public async Task UpdateAnalysisCategoryAsync(AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(analysisCategoryUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Категория анализов не зарегистрирована",
                                                        new Dictionary<object, object>()
                                                        {
                                                            {nameof(analysisCategoryUpdateDTO), analysisCategoryUpdateDTO}
                                                        });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
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


        public async Task DeleteAnalysisCategoryAsync(int analysisCategoryId)
        {
            _ = await _repository.GetByIdAsync(analysisCategoryId) ??
               throw new IncorrectOrEmptyResultException("Категория анализов не зарегистрирована",
                                                           new Dictionary<object, object>()
                                                           {
                                                                { nameof(analysisCategoryId), analysisCategoryId }
                                                           });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(analysisCategoryId);
        }
    }
}
