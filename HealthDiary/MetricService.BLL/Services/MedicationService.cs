using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class MedicationService(IMedicationRepository medicationRepository, IValidator<Medication> validator, ClaimsPrincipal authorizationService) : IMedicationService
    {
        private readonly IMedicationRepository _repository = medicationRepository;
        private readonly IValidator<Medication> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;

       
        public async Task CreateMedicationAsync(MedicationCreateDTO medicationCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            Medication medication = medicationCreateDTO.ToMedication();

            if (!_validator.Validate(medication, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о лекарстве", errorList);
            }

            await _repository.CreateAsync(medication);
        }

        
        public async Task DeleteMedicationAsync(int medicationId)
        {
            _ = await _repository.GetByIdAsync(medicationId) ??
              throw new IncorrectOrEmptyResultException("Лекарство не зарегистрировано", new Dictionary<object, object>()
              {
                    { "medicationId", medicationId }
              });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            await _repository.DeleteAsync(medicationId);
        }

        
        public async Task<IEnumerable<MedicationDTO>> GetAllMedicationAsync(int pageNum, int pageSize)
        {
            var medications = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize).ToMedicationDTO();

            return medications;
        }

              
        public async Task<MedicationDTO> GetMedicationByIdAsync(int medicationId)
        {
            return (await _repository.GetByIdAsync(medicationId) ??
              throw new IncorrectOrEmptyResultException("Указанное лекарство не существует", new Dictionary<object, object>()
            {
                   { "medicationId", medicationId }
            })).ToMedicationDTO();
        }

        
        public async Task UpdateMedicationAsync(MedicationUpdateDTO medicationUpdateDTO)
        {
            var medicationFind = await _repository.GetByIdAsync(medicationUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Лекарство не зарегистрировано",
                    new Dictionary<object, object>()
                    {
                        {"medicationUpdateDTO", medicationUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            medicationFind = medicationUpdateDTO.ToMedication(medicationFind.DosageFormId);

            if (!_validator.Validate(medicationFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о лекарстве", errorList);
            }

            await _repository.UpdateAsync(medicationFind);
        }
    }
}
