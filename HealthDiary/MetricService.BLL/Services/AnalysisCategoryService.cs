using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class AnalysisCategoryService(IAnalysisCategoryRepository repository, IValidator<AnalysisCategory> validator, ClaimsPrincipal authorizationService) : IAnalysisCategoryService
    {
        private readonly IAnalysisCategoryRepository _repository = repository;
        private readonly IValidator<AnalysisCategory> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;


        public async Task<IEnumerable<AnalysisCategoryDTO>> GetAllAnalysisCategoriesAsync(int pageNum, int pageSize)
        {
            var analysisCategories = (await _repository.GetAllAsync())
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToAnalysisCategoryDTO();

            return analysisCategories;
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
            return analysisCategory.ToAnalysisCategoryDTO();
        }


        public async Task<IEnumerable<AnalysisCategoryDTO>> GetListAnalysisCategoriesBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');
            var workrecords = await _repository.GetAllAsync();
            var filterrecords = new List<AnalysisCategoryDTO>();
            foreach (var item in stringsSearch)
            {
                filterrecords.AddRange(workrecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    .ToList()
                    .ToAnalysisCategoryDTO());
            }

            return filterrecords;
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

            AnalysisCategory analysisCategory = analysisCategoryCreateDTO.ToAnalysisCategory();

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

            var analysisCategoryFind = analysisCategoryUpdateDTO.ToAnalysisCategory();

            if (!_validator.Validate(analysisCategoryFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о категории анализов", errorList);
            }

            await _repository.UpdateAsync(analysisCategoryFind);
        }


        public async Task DeleteAnalysisCategoryAsync(int analysisCategoryId)
        {
            _ = await _repository.GetByIdAsync(analysisCategoryId) ??
               throw new IncorrectOrEmptyResultException("Категория анализов не зарегистрирована", new Dictionary<object, object>()
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
