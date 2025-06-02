namespace UserService.BLL.Dto
{
    /// <summary>
    /// Представляет роль пользователя в системе.
    /// Содержит уникальный идентификатор, название роли и коллекцию пользователей, которым назначена эта роль.
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Получает или задаёт уникальный идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задаёт название роли (например, "User", "Admin").
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
