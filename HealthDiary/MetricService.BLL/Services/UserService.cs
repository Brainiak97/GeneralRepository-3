using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
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
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>    
        /// <exception cref="IncorrectOrEmptyResultException">Возникает если указанный пользователь не существует</exception>   
        public async Task DeleteProfileAsync(int userId)
        {
            if (await _repository.GetByIdAsync(userId) == null)
                throw new IncorrectOrEmptyResultException("Указанный пользователь не существует", new Dictionary<object, object>()
                {
                    { "userId", userId }
                });

            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свой профиль", 
                    Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            await _repository.DeleteAsync(userId);

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

            var users = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize).ToUserDTO();                     

            return users;
        }

        /// <summary>
        /// получение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>  
        /// <exception cref="IncorrectOrEmptyResultException">Возникает когда пользователь с заданным ИД не найден</exception> 
        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var findUser = await _repository.GetByIdAsync(userId)??
                throw new IncorrectOrEmptyResultException("Пользователь не найден",
                new Dictionary<object, object>()
                {
                    {"userId", userId},
                });

            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете просматривать профиль другого пользователя", 
                    Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            return findUser.ToUserDTO();
        }

        /// <summary>
        /// Создание профиля пользователя
        /// </summary>
        /// /// <param name="author"></param>
        /// <param name="userDTO"></param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        ///  <exception cref="IncorrectOrEmptyResultException">Возникает когда пользователь уже зарегистрирован</exception>
        public async Task CreateProfileAsync(UserDTO userDTO)
        {
            
            if (await _repository.GetByIdAsync(userDTO.Id) != null)
                throw new IncorrectOrEmptyResultException("Пользователь уже зарегистрирован",
                    new Dictionary<object, object>()
                    {
                        {"userDTO", userDTO}
                    });

            if (!_authorizationService.IsInRole("Admin") && userDTO.Id != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создать данные другого пользователя", 
                    Common.Common.GetAuthorId(_authorizationService), userDTO.Id, _repository.Name);
            }

            Domain.Models.User? user = userDTO.ToUser();

            if (!_validator.Validate(user!, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);
            }

            await _repository.CreateAsync(user);
        }

        /// <summary>
        /// Обновление профиля пользователя
        /// </summary>
        /// /// <param name="author"></param>
        /// <param name="userDTO"></param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task UpdateProfileAsync(UserDTO userDTO)
        {
            var findUser = await _repository.GetByIdAsync(userDTO.Id) ?? 
                throw new IncorrectOrEmptyResultException("Пользователь не зарегистрирован",
                new Dictionary<object, object>()
                {
                    {"userDTO", userDTO },
                });           

            if (!_authorizationService.IsInRole("Admin") && findUser.Id != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные другого пользователя", 
                    Common.Common.GetAuthorId(_authorizationService), findUser.Id, _repository.Name);
            }

            findUser.Id = userDTO.Id;
            findUser.Weight = userDTO.Weight;
            findUser.Height = userDTO.Height;
            findUser.DateOfBirth = userDTO.DateOfBirth;

            if (!_validator.Validate(findUser!, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);

            }

            await _repository.UpdateAsync(findUser);
        }
    }
}
