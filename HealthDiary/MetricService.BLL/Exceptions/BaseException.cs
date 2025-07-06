namespace MetricService.BLL.Exceptions
{
    /// <summary>
    /// Базовое исключение
    /// </summary>
    public abstract class BaseException : ApplicationException
    {
        /// <summary>
        /// Базовое исключение
        /// </summary>
        /// <param name="message">Сообщение об ошибки</param>
        /// <param name="errorDetail">Детали ошибки</param>
        protected BaseException(string message, Dictionary<object, object>? errorDetail = null) : base(message)
        {
            if (errorDetail != null)
            {
                foreach (var item in errorDetail)
                {
                    Data.Add(item.Key, item.Value);
                }
            }
        }
    }
}
