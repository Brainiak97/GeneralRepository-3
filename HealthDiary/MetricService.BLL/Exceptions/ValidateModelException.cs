namespace MetricService.BLL.Exceptions
{
    public class ValidateModelException: ApplicationException
    {
        /// <summary>
        /// Возникает если валидация модели не прошла
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="errorDetail">детализация ошибки</param>
        public ValidateModelException(string message, IDictionary <string, string> errorDetail): base( message)
        {
        ErrorDetail = errorDetail;     
        }
        public IDictionary<string, string> ErrorDetail { get; init; }
    }
}
