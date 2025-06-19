using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.Reminder;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class ReminderService(
        IReminderRepository remionderRepository, 
        IValidator<Reminder> validator, 
        ClaimsPrincipal authorizationService,
        IRegimenService regimenService
        ) : IReminderService
    {
        private readonly IReminderRepository _repository = remionderRepository;
        private readonly IValidator<Reminder> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IRegimenService _regimenService = regimenService;

        
        public async Task CreateReminderAsync(ReminderCreateDTO reminderCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Вы не можете создавать данные", 
                    Common.Common.GetAuthorId(_authorizationService), 
                    0, 
                    _repository.Name);
            }

            Reminder reminder = reminderCreateDTO.ToReminder();

            if (!_validator.Validate(reminder, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о напоминании", errorList);
            }

            await _repository.CreateAsync(reminder);
        }

        
        public async Task DeleteReminderAsync(int reminderId)
        {
            _ = await _repository.GetByIdAsync(reminderId) ??
              throw new IncorrectOrEmptyResultException("Напоминание не зарегистрировано", new Dictionary<object, object>()
              {
                    { "reminderId", reminderId }
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
                                    r.RemindAt <= requestListWithPeriodByRegimenIdDTO.EndDate)
                .Skip((requestListWithPeriodByRegimenIdDTO.NumPage - 1) * requestListWithPeriodByRegimenIdDTO.PageSize)
                .Take(requestListWithPeriodByRegimenIdDTO.PageSize)
                .ToReminderDTO();

            return reminders;
        }

        
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
                                    r.RemindAt <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize)
                .Take(requestListWithPeriodByIdDTO.PageSize)
                .ToReminderDTO();

            return reminders;
        }

       
        public async Task<ReminderDTO> GetReminderByIdAsync(int reminderId)
        {
            var reminderFind = await _repository.GetByIdAsync(reminderId) ??
                throw new IncorrectOrEmptyResultException("Указанное напоминание не существует", new Dictionary<object, object>()
                {
                    { "reminderId", reminderId }
                });

            if (!_authorizationService.IsInRole("Admin") && reminderFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку", 
                    Common.Common.GetAuthorId(_authorizationService),
                    reminderFind.Regimen.UserId, 
                    _repository.Name);
            }

            return reminderFind.ToReminderDTO();
        }

        
        public async Task UpdateReminderAsync(ReminderUpdateDTO reminderUpdateDTO)
        {
            var reminderFind = await _repository.GetByIdAsync(reminderUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Напоминание не зарегистрировано",
                    new Dictionary<object, object>()
                    {
                        {"reminderUpdateDTO", reminderUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin") && reminderFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService),
                    reminderFind.Regimen.UserId, 
                    _repository.Name);
            }

            reminderFind = reminderUpdateDTO.ToReminder(reminderFind.RegimenId);

            if (!_validator.Validate(reminderFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о напоминании", errorList);
            }

            await _repository.UpdateAsync(reminderFind);
        }
    }
}
