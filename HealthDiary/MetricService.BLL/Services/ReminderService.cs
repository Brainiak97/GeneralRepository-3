using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Reminder;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о напоминании о приеме медикаментов пользователем
    /// </summary>
    /// <seealso cref="IReminderService" />
    public class ReminderService(IReminderRepository remionderRepository, IValidator<Reminder> validator, ClaimsPrincipal authorizationService, IRegimenService regimenService, IMapper mapper) : IReminderService
    {
        private readonly IReminderRepository _repository = remionderRepository;
        private readonly IValidator<Reminder> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IRegimenService _regimenService = regimenService;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task CreateReminderAsync(ReminderCreateDTO reminderCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            var reminder = _mapper.Map<Reminder>(reminderCreateDTO);

            if (!_validator.Validate(reminder, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о напоминании", errorList);
            }

            await _repository.CreateAsync(reminder);
        }


        /// <inheritdoc/>
        public async Task DeleteReminderAsync(int reminderId)
        {
            _ = await _repository.GetByIdAsync(reminderId) ??
              throw new IncorrectOrEmptyResultException("Напоминание не зарегистрировано",
                                                          new Dictionary<object, object>()
                                                          {
                                                                { nameof(reminderId), reminderId }
                                                          });

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вам не разрешено удалить данные",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(reminderId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<ReminderDTO>> GetAllReminderByRegimenIdAsync(RequestListWithPeriodByRegimenIdDTO requestListWithPeriodByRegimenIdDTO)
        {
            var regimen = await _regimenService.GetRegimenByIdAsync(requestListWithPeriodByRegimenIdDTO.RegimenId);

            if (!_authorizationService.IsInRole("Admin") &&
                regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои напоминания",
                    Common.Common.GetAuthorId(_authorizationService), regimen.UserId, _repository.Name);
            }

            var reminders = (await _repository.GetAllAsync())
                .Where(r => r.RegimenId == requestListWithPeriodByRegimenIdDTO.RegimenId &&
                                    r.RemindAt >= requestListWithPeriodByRegimenIdDTO.BegDate &&
                                    r.RemindAt <= requestListWithPeriodByRegimenIdDTO.EndDate);


            return _mapper.Map<IEnumerable<ReminderDTO>>(reminders);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<ReminderDTO>> GetAllReminderByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") &&
                requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои напоминания",
                    Common.Common.GetAuthorId(_authorizationService), requestListWithPeriodByIdDTO.UserId, _repository.Name);
            }

            var reminders = (await _repository.GetAllAsync())
                .Where(r => r.Regimen.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    r.RemindAt >= requestListWithPeriodByIdDTO.BegDate &&
                                    r.RemindAt <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<ReminderDTO>>(reminders);
        }


        /// <inheritdoc/>
        public async Task<ReminderDTO> GetReminderByIdAsync(int reminderId)
        {
            var reminderFind = await _repository.GetByIdAsync(reminderId) ??
                throw new IncorrectOrEmptyResultException("Указанное напоминание не существует",
                                                        new Dictionary<object, object>()
                                                        {
                                                            { nameof(reminderId), reminderId }
                                                        });

            if (!_authorizationService.IsInRole("Admin") && reminderFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    reminderFind.Regimen.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<ReminderDTO>(reminderFind);
        }


        /// <inheritdoc/>
        public async Task UpdateReminderAsync(ReminderUpdateDTO reminderUpdateDTO)
        {
            var reminderFind = await _repository.GetByIdAsync(reminderUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Напоминание не зарегистрировано",
                                                        new Dictionary<object, object>()
                                                        {
                                                            {nameof(reminderUpdateDTO), reminderUpdateDTO}
                                                        });

            if (!_authorizationService.IsInRole("Admin") && reminderFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    reminderFind.Regimen.UserId,
                                                    _repository.Name);
            }

            var reminder = _mapper.Map<Reminder>(reminderUpdateDTO);
            reminder.RegimenId = reminderFind.RegimenId;

            if (!_validator.Validate(reminder, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о напоминании", errorList);
            }

            await _repository.UpdateAsync(reminder);
        }
    }
}
