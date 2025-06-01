using MetricService.BLL.Dto;
using System.Security.Claims;

namespace MetricService.BLL.Interfaces
{
   public  interface IUserService
    {
        /// <summary>
        /// Создание антропометрии пользователя
        /// </summary>
        /// <param name="useruserDTO"></param>
        /// <returns></returns>
        public Task<bool> CreateProfileAsync(UserDTO useruserDTO);

        /// <summary>
        /// Обновление антропометрии пользователя
        /// </summary>
        /// <param name="useruserDTO"></param>
        /// <returns></returns>
        public Task<bool> UpdateProfileAsync(UserDTO useruserDTO);

        /// <summary>
        /// Удаление профиля пользователя из системы
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> DeleteProfileAsync(int userId);


        /// <summary>
        /// получение пользователя по ИД
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<UserDTO?> GetUserByIdAsync(int userId);

        /// <summary>
        /// Возвращает список пользователей системы
        /// </summary>
        /// <param name="pageNum">Номер страницы пагинации</param>
        /// <param name="pageSize">Количество записей га странице</param>
        /// <returns></returns>
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync(int pageNum, int pageSize);
                

    }
}
