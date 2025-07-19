using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    /// <summary>
    /// Предоставляет реализацию бизнес-логики для работы с данными профиля пользователя
    /// </summary>
    /// <seealso cref="IUserService" />
    public class UserService(IUserRepository userRepository, IValidator<Domain.Models.User> validator,
        ClaimsPrincipal authorizationService, IMapper mapper, IAccessToMetricsService accessToMetricsService) : IUserService
    {
        private readonly IUserRepository _repository = userRepository;
        private readonly IValidator<Domain.Models.User> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;
        private readonly IMapper _mapper = mapper;
        private readonly IAccessToMetricsService _accessToMetricsService = accessToMetricsService;


        /// <inheritdoc/>
        public async Task DeleteProfileAsync(int userId)
        {
            if (await _repository.GetByIdAsync(userId) == null)
                throw new IncorrectOrEmptyResultException("Указанный пользователь не существует",
                                                            new Dictionary<object, object>()
                                                            {
                                                                { nameof(userId), userId }
                                                            });

            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свой профиль",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    userId,
                                                    _repository.Name);
            }

            await _repository.DeleteAsync(userId);

        }


        /// <inheritdoc/>
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {

            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Только администраторам разрешено просматривать список пользователей",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    0,
                                                    _repository.Name);
            }

            return _mapper.Map<IEnumerable<UserDTO>>(await _repository.GetAllAsync());

        }


        /// <inheritdoc/>
        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var findUser = await _repository.GetByIdAsync(userId) ??
                throw new IncorrectOrEmptyResultException("Пользователь не найден",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(userId), userId},
                                                            });

            int grantedUserId = Common.Common.GetAuthorId(_authorizationService);

            if (!_authorizationService.IsInRole("Admin") && userId != grantedUserId &&
                            await _accessToMetricsService.CheckAccessToMetricsAsync(userId, grantedUserId) == false)
            {
                throw new ViolationAccessException("Вы не можете просматривать профиль другого пользователя",
                                                    grantedUserId,
                                                    userId,
                                                    _repository.Name);
            }

            return _mapper.Map<UserDTO>(findUser);
        }


        /// <inheritdoc/>
        public async Task CreateProfileAsync(UserDTO userDTO)
        {
            if (await _repository.GetByIdAsync(userDTO.Id) != null)
                throw new IncorrectOrEmptyResultException("Пользователь уже зарегистрирован",
                                                            new Dictionary<object, object>()
                                                            {
                                                                {nameof(userDTO), userDTO}
                                                            });

            if (!_authorizationService.IsInRole("Admin") && userDTO.Id != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создать данные другого пользователя",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    userDTO.Id,
                                                    _repository.Name);
            }

            var user = _mapper.Map<User>(userDTO);

            if (!_validator.Validate(user, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);
            }

            await _repository.CreateAsync(user);
        }


        /// <inheritdoc/>
        public async Task UpdateProfileAsync(UserDTO userDTO)
        {
            var findUser = await _repository.GetByIdAsync(userDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Пользователь не зарегистрирован",
                                                        new Dictionary<object, object>()
                                                        {
                                                            {nameof(userDTO), userDTO },
                                                        });

            if (!_authorizationService.IsInRole("Admin") && findUser.Id != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные другого пользователя",
                                                    Common.Common.GetAuthorId(_authorizationService),
                                                    findUser.Id,
                                                    _repository.Name);
            }

            var user = _mapper.Map<User>(userDTO);

            if (!_validator.Validate(user, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);

            }

            await _repository.UpdateAsync(user);
        }
    }
}
