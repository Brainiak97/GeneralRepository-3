using MetricService.BLL.Dto;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class UserService(IUserRepository userRepository, IValidator<Domain.Models.User> validator, ClaimsPrincipal authorizationService) : IUserService
    {
        private readonly IUserRepository _repository = userRepository;
        private readonly IValidator<Domain.Models.User> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;

        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>        
        public Task<bool> DeleteProfileAsync(int userId)
        {
            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свой профиль", Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            return _repository.DeleteAsync(userId);
        }

        /// <summary>
        /// Вывести список пользователей с пагинацией
        /// </summary>
        /// <param name="pageNum">номер страницы</param>
        /// <param name="pageSize">кол-во позиций на странице</param>
        /// <returns>Список моделей DTO</returns>,
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>        
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(int pageNum, int pageSize)
        {
            if (!_authorizationService.IsInRole("Admin"))
            {
                throw new ViolationAccessException("Только администраторам разрешено просматривать список пользователей", Common.Common.GetAuthorId(_authorizationService), 0, _repository.Name);
            }

            var users = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize);

            var usersDTO = new List<UserDTO>();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    usersDTO.Add(CreateUserDTO(user)!);
                }
            }
            return usersDTO;
        }

        /// <summary>
        /// получение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>        
        public async Task<UserDTO?> GetUserByIdAsync(int userId)
        {
            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете просматривать профиль другого пользователя", Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            return CreateUserDTO(await _repository.GetByIdAsync(userId));
        }

        /// <summary>
        /// Создание профиля пользователя
        /// </summary>
        /// /// <param name="author"></param>
        /// <param name="userDTO"></param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> CreateProfileAsync(UserDTO userDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && userDTO.Id != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создать данные другого пользователя", Common.Common.GetAuthorId(_authorizationService), userDTO.Id, _repository.Name);
            }

            Domain.Models.User? user = CreateUser(userDTO);

            if (!_validator.Validate(user!, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);
            }

            return await _repository.CreateAsync(user);
        }

        /// <summary>
        /// Обновление профиля пользователя
        /// </summary>
        /// /// <param name="author"></param>
        /// <param name="userDTO"></param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> UpdateProfileAsync(UserDTO userDTO)
        {
            var findUser = await _repository.GetByIdAsync(userDTO.Id);
            if (findUser == null) return false;

            if (!_authorizationService.IsInRole("Admin") && findUser.Id != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные другого пользователя", Common.Common.GetAuthorId(_authorizationService), findUser.Id, _repository.Name);
            }

            findUser.Id = userDTO.Id;
            findUser.Weight = userDTO.Weight;
            findUser.Height = userDTO.Height;
            findUser.DateOfBirth = userDTO.DateOfBirth;

            if (!_validator.Validate(findUser!, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);

            }
            
            return await _repository.UpdateAsync(findUser);
        }

        /// <summary>
        /// Создание UserDTO по модели пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Модель DTO</returns>
        private static UserDTO? CreateUserDTO(Domain.Models.User? user)
        {
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                DateOfBirth = user.DateOfBirth,
                Height = user.Height,
                Weight = user.Weight,
                Age = user.Age,
            };
        }


        /// <summary>
        /// Создание модели из DTO
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Модлель</returns>
        private static Domain.Models.User CreateUser(UserDTO userDTO)
        {
            return new Domain.Models.User
            {
                Id = userDTO.Id,
                DateOfBirth = userDTO.DateOfBirth,
                Height = userDTO.Height,
                Weight = userDTO.Weight
            };
        }

    }
}
