﻿using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Intake;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными о приеме медикаментов пользователем
    /// </summary>
    /// <seealso cref="IIntakeService" />
    public class IntakeService(IIntakeRepository intakeRepository, IValidator<Intake> validator, ClaimsPrincipal authorizationService, IRegimenService regimenService, IMapper mapper
        ) : IIntakeService
    {
        private readonly IIntakeRepository _repository = intakeRepository;
        private readonly IValidator<Intake> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IRegimenService _regimenService = regimenService;
        private readonly IMapper _mapper = mapper;


        /// <inheritdoc/>
        public async Task CreateIntakeAsync(IntakeCreateDTO intakeCreateDTO)
        {
            var regimen = await _regimenService.GetRegimenByIdAsync(intakeCreateDTO.RegimenId);
            if (!_authorizationService.IsInRole("Admin") && regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    regimen.UserId,
                                                    _repository.Name);
            }

            var intake = _mapper.Map<Intake>(intakeCreateDTO);

            if (!_validator.Validate(intake, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о приеме лекарств", errorList);
            }

            await _repository.CreateAsync(intake);
        }


        /// <inheritdoc/>
        public async Task DeleteIntakeAsync(int intakeId)
        {
            var intakeFind = await _repository.GetByIdAsync(intakeId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись приема лекарств не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(intakeId), intakeId }
                                                            });

            if (!_authorizationService.IsInRole("Admin") && intakeFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись приема лекарств",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    intakeFind.Regimen.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(intakeId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<IntakeDTO>> GetAllIntakeByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи приема лекарств",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    requestListWithPeriodByIdDTO.UserId,
                                                    _repository.Name);
            }

            var intakes = (await _repository.GetAllAsync())
                                            .Where(i => i.Regimen.UserId == requestListWithPeriodByIdDTO.UserId &&
                                                    i.TakenAt >= requestListWithPeriodByIdDTO.BegDate &&
                                                    i.TakenAt <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<IntakeDTO>>(intakes);
        }


        /// <inheritdoc/>
        public async Task<IntakeDTO> GetIntakeByIdAsync(int intakeId)
        {
            var intakeFind = await _repository.GetByIdAsync(intakeId) ??
               throw new IncorrectOrEmptyResultException("Указанная запись приема лекарств не существует",
                                                       new Dictionary<object, object>()
                                                       {
                                                            { nameof(intakeId), intakeId }
                                                       });

            if (!_authorizationService.IsInRole("Admin") && intakeFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи приема лекарств",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    intakeFind.Regimen.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<IntakeDTO>(intakeFind);
        }


        /// <inheritdoc/>
        public async Task UpdateIntakeAsync(IntakeUpdateDTO intakeUpdateDTO)
        {
            var intakeFind = await _repository.GetByIdAsync(intakeUpdateDTO.Id) ??
               throw new IncorrectOrEmptyResultException("Запись приема лекарств не зарегистрирована",
                                                           new Dictionary<object, object>()
                                                           {
                                                                {nameof(intakeUpdateDTO), intakeUpdateDTO}
                                                           });

            if (!_authorizationService.IsInRole("Admin") && intakeFind.Regimen.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    intakeFind.Regimen.UserId,
                                                    _repository.Name);
            }

            var intake = _mapper.Map<Intake>(intakeUpdateDTO);
            intake.RegimenId = intakeFind.RegimenId;

            if (!_validator.Validate(intake, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о записи приема лекарств", errorList);
            }

            await _repository.UpdateAsync(intake);
        }
    }
}
