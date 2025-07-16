namespace MetricService.Domain.Models
{
    /// <summary>
    /// Доступ к личным метрикам пользователя для других пользователей
    /// </summary>
    public class AccessToMetrics: BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя, предоставляющий доступ к своим метрикам
        /// </summary>       
        public int ProviderUserId {  get; set; }

        /// <summary>
        /// Пользователь, предоставляющий доступ к своим метрикам
        /// </summary>        
        public User ProviderUser { get; set; } = null!;       

        /// <summary>
        /// Идентификатор пользователя, которому предоставлен доступ к метрикам пользователя
        /// </summary>        
        public int GrantedUserId { get; set; }

        /// <summary>
        /// Пользователь, которому предоставлен доступ к метрикам пользователя
        /// </summary>       
        public User GrantedUser { get; set; } = null!;

        /// <summary>
        /// Дата, до которой включительно действует доступ личным метрикам
        /// </summary>        
        public DateOnly? AccessExpirationDate { get; set; }

        /// <summary>
        /// Доступ к метрикам без ограничения по скрокам
        /// </summary>
        /// <value>
        ///   <c>true</c> если доступ к метрикам постоянный; иначе, <c>false</c>.
        /// </value>
        public bool IsPermanentAccess { get; set; } 
    }
}
