using System.Security.Claims;

namespace MetricService.BLL.Common
{
    public static class Common
    {

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
