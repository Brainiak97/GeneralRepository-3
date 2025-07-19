using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricService.Domain.Models
{
    /// <summary>
    /// Доступ к личным метрикам пользователя для других пользователей
    /// </summary>
    [Comment("Доступ к личным метрикам")]
    public class AccessToMetrics: BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя, предоставляющий доступ к своим метрикам
        /// </summary> 
        [Comment("Идентификатор пользователя, который предоставляет доступ")]        
        public int ProviderUserId {  get; set; }

        /// <summary>
        /// Пользователь, предоставляющий доступ к своим метрикам
        /// </summary> 
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public User ProviderUser { get; set; } = null!;

        /// <summary>
        /// Идентификатор пользователя, которому предоставлен доступ к метрикам пользователя
        /// </summary> 
        [Comment("Идентификатор пользователя, которому предоставляется доступ")]        
        public int GrantedUserId { get; set; }

        /// <summary>
        /// Пользователь, которому предоставлен доступ к метрикам пользователя
        /// </summary>     
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public User GrantedUser { get; set; } = null!;

        /// <summary>
        /// Дата, до которой включительно действует доступ личным метрикам
        /// </summary>  
        [Comment("Дата, до которой действует доступ")]
        [Column(TypeName ="DATE")]
        public DateOnly? AccessExpirationDate { get; set; }

        /// <summary>
        /// Доступ к метрикам без ограничения по скрокам
        /// </summary>
        /// <value>
        ///   <c>true</c> если доступ к метрикам постоянный; иначе, <c>false</c>.
        /// </value>
        [Comment("Доступ без ограничения по срокам")]
        public bool IsPermanentAccess { get; set; } 
    }
}
