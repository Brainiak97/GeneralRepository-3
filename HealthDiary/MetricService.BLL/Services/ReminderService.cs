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
    public class ReminderService(IReminderRepository remionderRepository, IValidator<Reminder> validator, ClaimsPrincipal authorization, IRegimenService regimenService, IMapper mapper) : IReminderService
    {
        private readonly IReminderRepository _repository = remionderRepository;
        private readonly IValidator<Reminder> _validator = validator;
        private readonly ClaimsPrincipal _authorization = authorization;
        private readonly IRegimenService _regimenService = regimenService;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task CreateReminderAsync(ReminderCreateDTO reminderCreateDTO)
        {
            var regimen = await _regimenService.GetRegimenByIdAsync(reminderCreateDTO.RegimenId);

            Common.Common.CheckAccessAndThrow(_authorization, regimen.UserId, _repository.Name);

            var reminder = _mapper.Map<Reminder>(reminderCreateDTO);

            if (!_validator.Validate(reminder, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о напоминании", errorList);
            }

            await _repository.CreateAsync(reminder);
        }

        /// <inheritdoc/>
        public async Task UpdateReminderAsync(ReminderUpdateDTO reminderUpdateDTO)
        {
            var reminderFind = await GetReminderByIdAsync(reminderUpdateDTO.Id);

            var reminder = _mapper.Map<Reminder>(reminderUpdateDTO);

            reminder.IsSend = reminderFind.IsSend;
            reminder.RegimenId = reminderFind.RegimenId;

            await _repository.UpdateAsync(reminder);
        }

        /// <inheritdoc/>
        public async Task DeleteReminderAsync(int reminderId)
        {
            _ = await GetReminderByIdAsync(reminderId);

            await _repository.DeleteAsync(reminderId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Reminder>> GetAllReminderByRegimenIdAsync(RequestListWithPeriodByRegimenIdDTO requestListWithPeriodByRegimenIdDTO)
        {
            var regimen = await _regimenService.GetRegimenByIdAsync(requestListWithPeriodByRegimenIdDTO.RegimenId);

            Common.Common.CheckAccessAndThrow(_authorization, regimen.UserId, _repository.Name);

            var reminders = (await _repository.GetAllAsync())
                .Where(r => r.RegimenId == requestListWithPeriodByRegimenIdDTO.RegimenId &&
                                    r.RemindAt >= requestListWithPeriodByRegimenIdDTO.BegDate &&
                                    r.RemindAt <= requestListWithPeriodByRegimenIdDTO.EndDate);


            return reminders;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Reminder>> GetAllReminderByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            Common.Common.CheckAccessAndThrow(_authorization, requestListWithPeriodByIdDTO.UserId, _repository.Name);

            var reminders = (await _repository.GetAllAsync())
                .Where(r => r.Regimen.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    r.RemindAt >= requestListWithPeriodByIdDTO.BegDate &&
                                    r.RemindAt <= requestListWithPeriodByIdDTO.EndDate);

            return reminders;
        }

        /// <inheritdoc/>
        public async Task<Reminder> GetReminderByIdAsync(int reminderId)
        {
            var reminderFind = await _repository.GetByIdAsync(reminderId)
                ?? throw new IncorrectOrEmptyResultException("Указанное напоминание не существует",
                    new Dictionary<object, object>()
                    {
                        { nameof(reminderId), reminderId }
                    });

            Common.Common.CheckAccessAndThrow(_authorization, reminderFind.Regimen.UserId, _repository.Name);

            return reminderFind;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Reminder>> ReminderDeliveryAsync(int userId)
        {
            var currentDate = DateTime.UtcNow;

            Common.Common.CheckAccessAndThrow(_authorization, userId, _repository.Name);

            var reminders = (await _repository.GetAllAsync())
                .Where(r => r.Regimen.UserId == userId
                        && r.RemindAt <= currentDate
                        && r.IsSend == false);
            var results = new List<Reminder>();
            foreach (var reminder in reminders)
            {
                {
                    reminder.IsSend = true;
                    results.Add(reminder);
                    await _repository.UpdateAsync(reminder);
                }
            }
            return results;
        }
    }
}
