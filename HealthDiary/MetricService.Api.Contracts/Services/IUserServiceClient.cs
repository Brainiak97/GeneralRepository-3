using MetricService.Api.Contracts.Dtos.User;
using Refit;

namespace MetricService.Api.Contracts.Services
{
    /// <summary>
    /// Предоставляет контракт для работы с данные о профиле пользователя системы
    /// </summary>
    public interface IUserServiceClient
    {
        const string Controller = "/User";
        /// <summary>
        /// Зарегистрировать новый профиль пользователя
        /// </summary>
        /// <param name="userDTO">Данные для регистрации профиля пользователя</param>
        /// <returns></returns>
        [Post($"{Controller}/{nameof(CreateProfile)}")]
        Task CreateProfile(UserDTO userDTO);

        /// <summary>
        /// Изменить данные профиля пользователя
        /// </summary>
        /// <param name="userDTO">Измененные данные профиля пользователя</param>
        /// <returns></returns>
        [Put($"{Controller}/{nameof(UpdateProfile)}")]
        Task UpdateProfile(UserDTO userDTO);

        /// <summary>
        /// Удалить профиль пользователя и все его данные из системы
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        [Delete($"{Controller}/{nameof(DeleteProfile)}")]
        Task DeleteProfile(int userId);

        /// <summary>
        /// Получить профиль пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        [Get($"{Controller}/{nameof(GetUserById)}")]
        Task<UserDTO> GetUserById(int userId);
    }
}
