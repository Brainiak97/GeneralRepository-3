using AutoMapper;
using MetricService.BLL.DTO.DosageForm;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными справочника "Формы выпуска препарата"
    /// </summary>
    /// <seealso cref="IDosageFormService" />
    public class DosageFormService(IDosageFormRepository dosageFormRepository, ClaimsPrincipal authorizationService, IMapper mapper) : IDosageFormService
    {
        private readonly IDosageFormRepository _repository = dosageFormRepository;        
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task CreateDosageFormAsync(DosageFormCreateDTO dosageFormCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            var dosageForm = _mapper.Map<DosageForm>(dosageFormCreateDTO);            

            await _repository.CreateAsync(dosageForm);
        }


        /// <inheritdoc/>
        public async Task DeleteDosageFormAsync(int dosageFormId)
        {
            _ = await _repository.GetByIdAsync(dosageFormId) ??
               throw new IncorrectOrEmptyResultException("Форма выпуска не зарегистрирована",
                                                       new Dictionary<object, object>()
                                                       {
                                                            {nameof(dosageFormId), dosageFormId }
                                                       });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(dosageFormId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<DosageFormDTO>> GetAllDosageFormsAsync()
        {
            return _mapper.Map<IEnumerable<DosageFormDTO>>(await _repository.GetAllAsync());
        }


        /// <inheritdoc/>        
        public async Task<DosageFormDTO> GetDosageFormByIdAsync(int dosageFormId)
        {
            var dosageForm = (await _repository.GetByIdAsync(dosageFormId) ??
                throw new IncorrectOrEmptyResultException("Форма выпуска не существует",
                                                          new Dictionary<object, object>()
                                                          {
                                                               { nameof(dosageFormId), dosageFormId }
                                                          }));
            return _mapper.Map<DosageFormDTO>(dosageForm);
        }


        /// <inheritdoc/>
        public async Task UpdateDosageFormAsync(DosageFormUpdateDTO dosageFormUpdateDTO)
        {
            _ = await _repository.GetByIdAsync(dosageFormUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Форма выпуска не зарегистрирована",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(dosageFormUpdateDTO), dosageFormUpdateDTO}
                                                            });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете изменять данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            var dosageForm = _mapper.Map<DosageForm>(dosageFormUpdateDTO);           

            await _repository.UpdateAsync(dosageForm);
        }
    }
}
