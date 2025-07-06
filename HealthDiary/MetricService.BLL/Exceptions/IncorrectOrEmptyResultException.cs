namespace MetricService.BLL.Exceptions
{
    /// <summary>
    /// Возникает когда из репозитория возвращается пустой результат или ложь
    /// при модификации данных в репозитории
    /// </summary>
    public class IncorrectOrEmptyResultException : BaseException
    {
        /// <summary>
        /// Возникает когда из репозитория возвращается пустой результат или ложь
        /// при модификации данных в репозитории
        /// </summary>
        /// <param name="message">сообщение об ошибке</param>
        /// <param name="errorDetail">исходные данные запроса, которые привели к ошибке</param>
        public IncorrectOrEmptyResultException(string message, Dictionary<object, object>? errorDetail = null) : base(message, errorDetail)
        {
        }        
    }
}
