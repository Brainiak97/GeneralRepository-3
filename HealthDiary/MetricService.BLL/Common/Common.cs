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
    }
}
