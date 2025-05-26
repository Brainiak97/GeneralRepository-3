namespace MetricService.BLL.Exceptions
{
    internal class ValidateModelException: ApplicationException
    {
        public ValidateModelException(string message, IDictionary <string, string> errorDetail): base( message)
        {
        ErrorDetail = errorDetail;     
        }
        public IDictionary<string, string> ErrorDetail { get; init; }
    }
}
