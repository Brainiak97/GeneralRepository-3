using MetricService.BLL.Dto;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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
        /// <returns>true - если даление успешно</returns>
        /// <exception cref="ViolationAccessException"></exception>
        public async Task<bool> DeleteWorkoutAsync(int workoutId)
        {
            var workoutFind = await _repository.GetByIdAsync(workoutId);
            if (workoutFind == null) return false;

            if (!_authorizationService.IsInRole("Admin") && workoutFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою тренировку", Common.Common.GetAuthorId(_authorizationService), workoutFind.UserId, _repository.Name);
            }

            return await _repository.DeleteAsync(workoutId);
        }


        /// <summary>
        /// Получить все тренировки для пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">начало периода выбора тренировок</param>
        /// <param name="endDate">конец периода выбора тренировок</param>
        /// <param name="pageNum">номер страницы при пагинации</param>
        /// <param name="pageSize">кол-во строк на странице при пагинации</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException"></exception>
        public async Task<IEnumerable<WorkoutDTO>> GetAllWorkoutsByUserIdAsync(int userId, DateTime begDate, DateTime endDate, int pageNum, int pageSize)
        {
            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои тренировки", Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            var workouts = (await _repository.GetAllAsync()).Where(w => w.UserId == userId && w.StartTime >= begDate && w.EndTime <= endDate)
                .Skip((pageNum - 1) * pageSize).Take(pageSize);

            var workoutsDTO = new List<WorkoutDTO>();
            if (workouts.Any())
            {
                foreach (var workout in workouts)
                {
                    workoutsDTO.Add(CreateWorkoutDTO(workout)!);
                }
            }
            return workoutsDTO;
        }


        /// <summary>
        /// Получить тренировку по ИД
        /// </summary>
        /// <param name="workoutId">ИД тренировки</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException"></exception>
        public async Task<WorkoutDTO?> GetWorkoutByWorkoutIdAsync(int workoutId)
        {
            var workoutFind = await _repository.GetByIdAsync(workoutId);
            if (workoutFind == null) return null;

            if (!_authorizationService.IsInRole("Admin") && workoutFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку", Common.Common.GetAuthorId(_authorizationService), workoutFind.UserId, _repository.Name);
            }

            return CreateWorkoutDTO(workoutFind);
        }


        /// <summary>
        /// Создание тренировки
        /// </summary>
        /// <param name="workoutDTO">тренировка</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> CreateWorkoutAsync(WorkoutDTO workoutDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && workoutDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о тренировке для других пользователей", Common.Common.GetAuthorId(_authorizationService), workoutDTO.UserId, _repository.Name);
            }

            Workout workout = CreateModelFromDTO(workoutDTO);

            if (!_validator.Validate(workout, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о тренировке пользователя", errorList);
            }
            return await _repository.CreateAsync(workout);
        }


        /// <summary>
        /// Обновить данные о тренировке
        /// </summary>
        /// <param name="workoutDTO">тренировка</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим тренировкам</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> UpdateWorkoutAsync(WorkoutDTO workoutDTO)
        {
            var findWorkout = await _repository.GetByIdAsync(workoutDTO.Id);
            if (findWorkout == null) return false;

            if (!_authorizationService.IsInRole("Admin") && findWorkout.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей", Common.Common.GetAuthorId(_authorizationService), findWorkout.UserId, _repository.Name);
            }

            findWorkout.Description = workoutDTO.Description;
            findWorkout.EndTime = workoutDTO.EndTime;
            findWorkout.StartTime = workoutDTO.StartTime;
            findWorkout.PhysicalActivityId = workoutDTO.PhysicalActivityId;

            if (!_validator.Validate(findWorkout, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о тренировке пользователя", errorList);
            }

            return await _repository.UpdateAsync(findWorkout);
        }


        /// <summary>
        /// Создание модели из DTO
        /// </summary>
        /// <param name="workoutDTO"></param>
        /// <returns></returns>
        private static Workout CreateModelFromDTO(WorkoutDTO workoutDTO)
        {
            return new Workout
            {
                Id = workoutDTO.Id,
                UserId = workoutDTO.UserId,
                Description = workoutDTO.Description,
                StartTime = workoutDTO.StartTime,
                EndTime = workoutDTO.EndTime,
                PhysicalActivityId = workoutDTO.PhysicalActivityId,
            };

        }


        private static float CaloriesBurned(Workout workout)
        {            
            return  (float)(workout.PhysicalActivity.EnergyEquivalent * workout.User.Weight * (workout.EndTime - workout.StartTime).Hours);            
        }



        /// <summary>
        /// Создание DTO из модели
        /// </summary>
        /// <param name="workout"></param>
        /// <returns></returns>
        private static WorkoutDTO? CreateWorkoutDTO(Workout? workout)
        {
            if (workout == null) return null;
            return new WorkoutDTO
            {
                Id = workout.Id,
                EndTime = workout.EndTime,
                StartTime = workout.StartTime,
                Description = workout.Description,
                PhysicalActivityId = workout.PhysicalActivityId,
                UserId = workout.UserId,
                CaloriesBurned = CaloriesBurned(workout)
            };
        }
    }
}
