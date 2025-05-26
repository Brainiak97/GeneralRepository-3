using MetricService.BLL.Dto;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;

namespace MetricService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<User> _validator;
        public UserService(IUserRepository userRepository, IValidator<User> validator)
        {
            _repository = userRepository;
            _validator = validator;
        }

        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> DeleteProfileAsync(int userId)
        {
            return _repository.DeleteAsync(userId);
        }

        /// <summary>
        /// Вывести список пользователей с пагинацией
        /// </summary>
        /// <param name="pageNum">номер страницы</param>
        /// <param name="pageSize">кол-во позиций на странице</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(int pageNum, int pageSize)
        {
            var users = (await _repository.GetAllAsync()).Skip((pageNum - 1) * pageSize).Take(pageSize);

            var usersDTO = new List<UserDTO>();
            if (users.Count() > 0)
            {
                foreach (var user in users)
                {
                   usersDTO.Add( CreateUserDTO(user));
                }                
            }
            return usersDTO;
        }

        /// <summary>
        /// получение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<UserDTO?> GetUserByIdAsync(int userId)
        {
            return CreateUserDTO(await _repository.GetByIdAsync(userId));
        }

        /// <summary>
        /// Обновление или создание профиля пользователя
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProfileAsync(UserDTO userDTO)
        {
            User user = new()
            {
                Id = userDTO.Id,
                DateOfBirth = userDTO.DateOfBirth,
                Height = userDTO.Height,
                Weight = userDTO.Weight
            };

            IDictionary<string, string> errorList;
            if (!_validator.Validate(user, out errorList))
            {
                throw new ValidateModelException("Некорректные данные пользователя", errorList);

            }

            var findUser = await _repository.GetByIdAsync(user.Id);
            if (findUser == null)
            {
                return await _repository.CreateAsync(user);
            }
            else
            {
                findUser.Id = user.Id;
                findUser.Weight = user.Weight;
                findUser.Height = user.Height;
                findUser.DateOfBirth = user.DateOfBirth;
                return await _repository.UpdateAsync(user);
            }

        }

        /// <summary>
        /// Создание UserDTO по модели пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserDTO? CreateUserDTO(User? user)
        {
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                DateOfBirth = DateTime.Now,
                Height = user.Height,
                Weight = user.Weight,
            };
        }
    }
}
