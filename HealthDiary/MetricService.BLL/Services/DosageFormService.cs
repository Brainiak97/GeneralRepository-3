using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class DosageFormService(IDosageFormRepository dosageFormRepository, IValidator<DosageForm> validator, ClaimsPrincipal authorizationService) : IDosageFormService
    {
        private readonly IDosageFormRepository _repository = dosageFormRepository;
        private readonly IValidator<DosageForm> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;

        
        public async Task CreateDosageFormAsync(DosageFormCreateDTO dosageFormCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            DosageForm dosageForm = dosageFormCreateDTO.ToDosageForm();

            if (!_validator.Validate(dosageForm, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о форме выпуска препарата", errorList);
            }

            await _repository.CreateAsync(dosageForm);
        }

        
        public async Task DeleteDosageFormAsync(int dosageFormId)
        {
            _ = await _repository.GetByIdAsync(dosageFormId) ??
               throw new IncorrectOrEmptyResultException("Форма выпуска не зарегистрирована", new Dictionary<object, object>()
               {
                    { "dosageFormId", dosageFormId }
               });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            await _repository.DeleteAsync(dosageFormId);
        }

        
        public async Task<IEnumerable<DosageFormDTO>> GetAllDosageFormsAsync(int pageNum, int pageSize)
        {
            var dosageForms = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize).ToAnalysisCategoryDTO();

            return dosageForms;
        }

             
        public async Task<DosageFormDTO> GetDosageFormByIdAsync(int dosageFormId)
        {
            return (await _repository.GetByIdAsync(dosageFormId) ??
                throw new IncorrectOrEmptyResultException("Форма выпуска не существует", new Dictionary<object, object>()
              {
                   { "dosageFormId", dosageFormId }
              })).ToDosageFormDTO();
        }

        
        public async Task UpdateDosageFormAsync(DosageFormUpdateDTO dosageFormUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(dosageFormUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Форма выпуска не зарегистрирована",
                    new Dictionary<object, object>()
                    {
                        {"dosageFormUpdateDTO", dosageFormUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            var dosageFormFind = dosageFormUpdateDTO.ToDosageForm();

            if (!_validator.Validate(dosageFormFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о форме выпуска", errorList);
            }

            await _repository.UpdateAsync(dosageFormFind);
        }
    }
}
