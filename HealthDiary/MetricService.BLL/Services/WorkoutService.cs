﻿using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class WorkoutService(IWorkoutRepository workoutRepository, IValidator<Workout> validator, ClaimsPrincipal authorizationService) : IWorkoutService
    {
        private readonly IWorkoutRepository _repository = workoutRepository;
        private readonly IValidator<Workout> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;

        /// <summary>
        /// Удалить тренировку
        /// </summary>
        /// <param name="workoutId">ИД тренировки</param>       
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная тренировка не существует</exception>
        public async Task DeleteWorkoutAsync(int workoutId)
        {
            var workoutFind = await _repository.GetByIdAsync(workoutId) ??
                throw new IncorrectOrEmptyResultException("Указанная тренировка не существует", new Dictionary<object, object>()
                {
                    { "workoutId", workoutId }
                });

            if (!_authorizationService.IsInRole("Admin") && workoutFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою тренировку", Common.Common.GetAuthorId(_authorizationService), workoutFind.UserId, _repository.Name);
            }

            await _repository.DeleteAsync(workoutId);
        }

        /// <summary>
        /// Получить все тренировки для пользователя
        /// </summary>
        /// <param cref="RequestListWithPeriodByIdDTO">Запрос</param>        
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public async Task<IEnumerable<WorkoutDTO>> GetAllWorkoutsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои тренировки", 
                    Common.Common.GetAuthorId(_authorizationService), requestListWithPeriodByIdDTO.UserId, _repository.Name);
            }

            var workouts = (await _repository.GetAllAsync()).Where(w => w.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    w.StartTime >= requestListWithPeriodByIdDTO.BegDate && w.EndTime <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize).Take(requestListWithPeriodByIdDTO.PageSize).ToWorkoutDTO();

            return workouts;
        }

        /// <summary>
        /// Получить тренировку по ИД
        /// </summary>
        /// <param name="workoutId">ИД тренировки</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная тренировка не существует</exception>
        public async Task<WorkoutDTO> GetWorkoutByIdAsync(int workoutId)
        {
            var workoutFind = await _repository.GetByIdAsync(workoutId) ??
                throw new IncorrectOrEmptyResultException("Указанная тренировка не существует", new Dictionary<object, object>()
                {
                    { "workoutId", workoutId }
                });

            if (!_authorizationService.IsInRole("Admin") && workoutFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку", Common.Common.GetAuthorId(_authorizationService), workoutFind.UserId, _repository.Name);
            }

            return workoutFind.ToWorkoutDTO();
        }

        /// <summary>
        /// Создание тренировки
        /// </summary>
        /// <param name="workoutDTO">тренировка</param>       
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>        
        public async Task CreateWorkoutAsync(WorkoutCreateDTO workoutDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && workoutDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о тренировке для других пользователей", Common.Common.GetAuthorId(_authorizationService), workoutDTO.UserId, _repository.Name);
            }

            Workout workout = workoutDTO.ToWorkout();

            if (!_validator.Validate(workout, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о тренировке пользователя", errorList);
            }

            await _repository.CreateAsync(workout);
        }

        /// <summary>
        /// Обновить данные о тренировке
        /// </summary>
        /// <param name="workoutDTO">тренировка</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="ValidateModelException">Тренировка не зарегистрирована</exception>
        public async Task UpdateWorkoutAsync(WorkoutUpdateDTO workoutDTO)
        {
            var findWorkout = await _repository.GetByIdAsync(workoutDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Тренировка не зарегистрирована",
                    new Dictionary<object, object>()
                    {
                        {"workoutDTO", workoutDTO}
                    });

            if (!_authorizationService.IsInRole("Admin") && findWorkout.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей", Common.Common.GetAuthorId(_authorizationService), findWorkout.UserId, _repository.Name);
            }

            findWorkout = workoutDTO.ToWorkout(findWorkout.UserId);

            if (!_validator.Validate(findWorkout, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о тренировке пользователя", errorList);
            }

            await _repository.UpdateAsync(findWorkout);
        }
    }
}
