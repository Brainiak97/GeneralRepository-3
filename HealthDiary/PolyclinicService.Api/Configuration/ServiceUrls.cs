namespace PolyclinicService.Api.Configuration
{
    public class ServiceUrls
    {
        /// <summary>
        /// Url сервиса пользовательских метрик.
        /// </summary>
        public string MetricServiceUrl { get; set; } = string.Empty;

        /// <summary>
        /// Url сервиса пользователей.
        /// </summary>
        public string UserServiceUrl { get; set; } = string.Empty;
    }
}
