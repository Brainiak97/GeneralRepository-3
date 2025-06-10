using UserService.Domain.Models;

namespace UserService.BLL.Dto
{
    /// <summary>
    /// Представляет данные для обновления информации о пользователе.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Gender Gender { get; set; }
    }
}
