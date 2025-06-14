namespace MetricService.BLL.Exceptions
{
    public class IncorrectOrEmptyResultException : BaseException
    {
        /// <summary>
        /// Возникает когда из репозитория возвращается пустой результ или ложь
        /// когда происходит модификация данных в репозитории
        /// </summary>
        /// <param name="message">сообщение об ошибке</param>
        /// <param name="errorDetail">исходные данные запроса, которые привели к ошибке</param>
        public IncorrectOrEmptyResultException(string message, Dictionary<object, object>? errorDetail = null) : base(message, errorDetail)
        {
        }        
    }
}
