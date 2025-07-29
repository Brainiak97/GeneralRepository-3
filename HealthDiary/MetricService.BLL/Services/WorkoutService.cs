using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Workout;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{

    /// <summary>
    ///  Предоставляет реализацию бизнес-логики для работы с данными о тренировках пользователя
    /// </summary>
    /// <seealso cref="IWorkoutService" />
    public class WorkoutService(IWorkoutRepository workoutRepository, IValidator<Workout> validator,
        ClaimsPrincipal authorizationService, IMapper mapper, IAccessToMetricsService accessToMetricsService) : IWorkoutService
    {
        private readonly IWorkoutRepository _repository = workoutRepository;
        private readonly IValidator<Workout> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;


        /// <inheritdoc/>
        public async Task DeleteWorkoutAsync(int workoutId)
        {
            var workoutFind = await _repository.GetByIdAsync(workoutId) ??
                throw new IncorrectOrEmptyResultException("Указанная тренировка не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(workoutId), workoutId }
                                                            });

            if (!_authorizationService.IsInRole("Admin") && workoutFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою тренировку",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    workoutFind.UserId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(workoutId);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<WorkoutDTO>> GetAllWorkoutsByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            int grantedUserId = Common.Common.GetAuthorId(_authorizationService);

            if (!_authorizationService.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(requestListWithPeriodByIdDTO.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои тренировки",
                                                    grantedUserId,
                                                    requestListWithPeriodByIdDTO.UserId,
                                                    _repository.Name);
            }

            var workouts = (await _repository.GetAllAsync())
                            .Where(w => w.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    w.StartTime >= requestListWithPeriodByIdDTO.BegDate &&
                                    w.EndTime <= requestListWithPeriodByIdDTO.EndDate);

            return _mapper.Map<IEnumerable<WorkoutDTO>>(workouts);
        }


        /// <inheritdoc/>
        public async Task<WorkoutDTO> GetWorkoutByIdAsync(int workoutId)
        {
            var workoutFind = await _repository.GetByIdAsync(workoutId) ??
                throw new IncorrectOrEmptyResultException("Указанная тренировка не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(workoutId), workoutId }
                                                            });

            int grantedUserId = Common.Common.GetAuthorId(_authorizationService);

            if (!_authorizationService.IsInRole("Admin") && workoutFind.UserId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(workoutFind.UserId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку",
                                                    grantedUserId,
                                                    workoutFind.UserId,
                                                    _repository.Name);
            }

            return _mapper.Map<WorkoutDTO>(workoutFind);
        }


        /// <inheritdoc/>
        public async Task CreateWorkoutAsync(WorkoutCreateDTO workoutDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && workoutDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    workoutDTO.UserId,
                                                    _repository.Name);
            }

            Workout workout = _mapper.Map<Workout>(workoutDTO);

            if (!_validator.Validate(workout, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о тренировке пользователя", errorList);
            }

            await _repository.CreateAsync(workout);
        }


        /// <inheritdoc/>
        public async Task UpdateWorkoutAsync(WorkoutUpdateDTO workoutUpdateDTO)
        {
            var findWorkout = await _repository.GetByIdAsync(workoutUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Тренировка не зарегистрирована",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(workoutUpdateDTO), workoutUpdateDTO}
                                                            });

            if (!_authorizationService.IsInRole("Admin") && findWorkout.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    findWorkout.UserId,
                                                    _repository.Name);
            }

            var updateWorkout = _mapper.Map<Workout>(workoutUpdateDTO);
            updateWorkout.UserId = findWorkout.UserId;

            if (!_validator.Validate(updateWorkout, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о тренировке пользователя", errorList);
            }

            await _repository.UpdateAsync(updateWorkout);
        }
    }
}
