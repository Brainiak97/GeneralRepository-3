namespace MetricService.BLL.Exceptions
{
    public class ValidateModelException : BaseException
    {
        /// <summary>
        /// Возникает если валидация модели не прошла
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="errorDetail">детализация ошибки</param>
        public ValidateModelException(string message, Dictionary<string, string>? errorDetail = null) : base(message)
        {
            if (errorDetail != null)
            {
                foreach (var error in errorDetail)
                {
                    Data.Add(error.Key, error.Value);   
                }
            }
        }

    }
}
