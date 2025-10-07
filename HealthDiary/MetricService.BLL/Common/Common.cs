using MetricService.BLL.Exceptions;
using System.Security.Claims;

namespace MetricService.BLL.Common
{
    /// <summary>
    /// класс общих функций
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Получение пользователя, от имени которого происходит действие
        /// </summary>
        /// <param name="author">Авторизованный пользователь</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetAuthorId(ClaimsPrincipal author)
        {
            if (author.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            {
                throw new Exception("В JWT не обозначен автор действия");
            }

            return Convert.ToInt32(author.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        /// <summary>
        /// Проверяет, может ли авторизованный пользователь работать с данными от имени другого пользователя.
        /// Если доступ отсутствует, то бросается исключение
        /// </summary>
        /// <param name="author">Авторизованный пользователь</param>
        /// <param name="targetUser">ИД пользователя, с данными которого собираемся работать</param>
        /// <param name="entity">Сущность, с данными которой собираемся работать</param>
        /// <exception cref="ViolationAccessException">Вы можете работать только со своими данными</exception>
        public static void CheckAccessAndThrow(ClaimsPrincipal author, int targetUser, string entity)
        {
            if (!author.IsInRole("Admin") && targetUser != GetAuthorId(author))
            {
                throw new ViolationAccessException(
                    "Вы можете работать только со своими данными",
                    GetAuthorId(author),
                    targetUser,
                    entity);
            }
        }
    }
}
