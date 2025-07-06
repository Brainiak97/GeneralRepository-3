using AutoMapper;
using MetricService.BLL.DTO.MedicationDTO;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными справочника "Медикаменты"
    /// </summary>
    /// <seealso cref="IMedicationService" />
    public class MedicationService(IMedicationRepository medicationRepository, ClaimsPrincipal authorizationService, IMapper mapper) : IMedicationService
    {
        private readonly IMedicationRepository _repository = medicationRepository;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task CreateMedicationAsync(MedicationCreateDTO medicationCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                    Common.Common.GetAuthorId(_authorizationService),
                    0,
                    _repository.Name);
            }

            var medication = _mapper.Map<Medication>(medicationCreateDTO);

            await _repository.CreateAsync(medication);
        }


        /// <inheritdoc/>
        public async Task DeleteMedicationAsync(int medicationId)
        {
            _ = await _repository.GetByIdAsync(medicationId) ??
              throw new IncorrectOrEmptyResultException("Лекарство не зарегистрировано",
                                                          new Dictionary<object, object>()
                                                          {
                                                                { nameof(medicationId), medicationId }
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


        /// <inheritdoc/>
        public async Task<IEnumerable<MedicationDTO>> GetAllMedicationAsync()
        {
            return _mapper.Map<IEnumerable<MedicationDTO>>(await _repository.GetAllAsync());
        }


        /// <inheritdoc/>          
        public async Task<MedicationDTO> GetMedicationByIdAsync(int medicationId)
        {
            var medication = (await _repository.GetByIdAsync(medicationId) ??
               throw new IncorrectOrEmptyResultException("Указанное лекарство не существует",
                                                       new Dictionary<object, object>()
                                                       {
                                                             { nameof(medicationId), medicationId }
                                                       }));

            return _mapper.Map<MedicationDTO>(medication);
        }


        /// <inheritdoc/>
        public async Task UpdateMedicationAsync(MedicationUpdateDTO medicationUpdateDTO)
        {
            var medicationFind = await _repository.GetByIdAsync(medicationUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Лекарство не зарегистрировано",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(medicationUpdateDTO), medicationUpdateDTO}
                                                            });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            var medication = _mapper.Map<Medication>(medicationUpdateDTO);
            medication.DosageFormId = medicationFind.DosageFormId;

            await _repository.UpdateAsync(medication);
        }
    }
}
