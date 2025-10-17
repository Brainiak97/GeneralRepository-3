using MetricService.BLL.DTO;
using MetricService.BLL.Exceptions;

namespace MetricService.BLL.Interfaces
{
    /// <summary>
    /// Определяет контракт для сервиса, работающего с данными о профиле пользователя
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создать запись профиля пользователя
        /// </summary>        
        /// <param name="userDTO">Данные для создания записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        ///  <exception cref="IncorrectOrEmptyResultException">Возникает когда пользователь уже зарегистрирован</exception>
        public Task CreateProfileAsync(UserDTO userDTO);

        /// <summary>
        /// Обновить запись профиля пользователя
        /// </summary>       
        /// <param name="userDTO">Данные для изменения записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public Task UpdateProfileAsync(UserDTO userDTO);

        /// <summary>
        /// Удалить запись профиля пользователя
        /// </summary>
        /// <param name="userId">Идентификатор записи</param>       
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>    
        /// <exception cref="IncorrectOrEmptyResultException">Возникает если указанный пользователь не существует</exception>   
        public Task DeleteProfileAsync(int userId);


        /// <summary>
        /// Получить запись профиля пользователя
        /// </summary>
        /// <param name="userId">Идентификатор записи</param>
        /// <returns>Данные профиля пользователя</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>  
        /// <exception cref="IncorrectOrEmptyResultException">Возникает когда пользователь с заданным ИД не найден</exception> 
        public Task<UserDTO> GetUserByIdAsync(int userId);
    }
}
