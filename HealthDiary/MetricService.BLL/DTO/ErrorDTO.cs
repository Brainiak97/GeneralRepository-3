namespace MetricService.BLL.DTO
{
    public class ErrorDTO
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; internal set; } = string.Empty;

        /// <summary>
        /// Детали ошибки
        /// </summary>
        public System.Collections.IDictionary DetailErrors { get; set; } = new Dictionary<object, object>();
    }
}
