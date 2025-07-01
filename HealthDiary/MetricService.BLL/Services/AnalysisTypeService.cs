using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class AnalysisTypeService(IAnalysisTypeRepository repository, IValidator<AnalysisType> validator, ClaimsPrincipal authorizationService) : IAnalysisTypeService
    {
        private readonly IAnalysisTypeRepository _repository = repository;
        private readonly IValidator<AnalysisType> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;

        public async Task<IEnumerable<AnalysisTypeDTO>> GetAllAnalysisTypeAsync(int pageNum, int pageSize)
        {
            var analysisTypes = (await _repository.GetAllAsync())
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToAnalysisTypeDTO();

            return analysisTypes;
        }        
        
        public async Task<AnalysisTypeDTO?> GetAnalysisTypeByIdAsync(int typeId)
        {

            return (await _repository.GetByIdAsync(typeId) ??
               throw new IncorrectOrEmptyResultException("Указанный тип анализов не существует", new Dictionary<object, object>()
             {
                   { nameof(typeId), typeId }
             })).ToAnalysisTypeDTO();
        }

        
        public async Task<IEnumerable<AnalysisTypeDTO>> GetListAnalysisTypeBySearchAsync(string search)
        {
            var stringsSearch = search.Split(',');
            var workrecords = await _repository.GetAllAsync();
            var filterrecords = new List<AnalysisTypeDTO>();
            foreach (var item in stringsSearch)
            {
                filterrecords.AddRange(workrecords.Where(s => s.Name.Contains(item.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList().ToAnalysisTypeDTO());
            }

            return filterrecords;
        }

        
        public async Task CreateAnalysisTypeAsync(AnalysisTypeCreateDTO analysisTypeCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            AnalysisType analysisType = analysisTypeCreateDTO.ToAnalysisType();

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
                        {"analysisTypeUpdateDTO", analysisTypeUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            var analysisTypeFind = analysisTypeUpdateDTO.ToAnalysisType();

            if (!_validator.Validate(analysisTypeFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о типе анализа", errorList);
            }

            await _repository.UpdateAsync(analysisTypeFind);
        }

        
        public async Task DeleteAnalysisTypeAsync(int analysisTypeId)
        {
            _ = await _repository.GetByIdAsync(analysisTypeId) ??
               throw new IncorrectOrEmptyResultException("Тип анализов не зарегистрирован", new Dictionary<object, object>()
               {
                    { "analysisTypeId", analysisTypeId }
               });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            await _repository.DeleteAsync(analysisTypeId);
        }
    }
}

