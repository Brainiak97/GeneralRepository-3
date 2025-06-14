using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
   public  interface IUserService
    {
        /// <summary>
        /// Создание профиля пользователя
        /// </summary>
        /// /// <param name="author"></param>
        /// <param name="userDTO"></param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        ///  <exception cref="IncorrectOrEmptyResultException">Возникает когда пользователь уже зарегистрирован</exception>
        public Task CreateProfileAsync(UserDTO userDTO);

        /// <summary>
        /// Обновление профиля пользователя
        /// </summary>
        /// /// <param name="author"></param>
        /// <param name="userDTO"></param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public Task UpdateProfileAsync(UserDTO userDTO);

        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="userId"></param>       
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>    
        /// <exception cref="IncorrectOrEmptyResultException">Возникает если указанный пользователь не существует</exception>   
        public Task DeleteProfileAsync(int userId);


        /// <summary>
        /// получение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>  
        /// <exception cref="IncorrectOrEmptyResultException">Возникает когда пользователь с заданным ИД не найден</exception> 
        public Task<UserDTO> GetUserByIdAsync(int userId);

        /// <summary>
        /// Вывести список пользователей с пагинацией
        /// </summary>
        /// <param name="pageNum">номер страницы</param>
        /// <param name="pageSize">кол-во позиций на странице</param>
        /// <returns>Список моделей DTO</returns>,
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>     
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync(int pageNum, int pageSize);
    }
}
