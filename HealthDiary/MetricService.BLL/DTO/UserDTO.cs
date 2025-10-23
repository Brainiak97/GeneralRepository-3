namespace MetricService.BLL.DTO
{
    /// <summary>
    /// Объект данных о профиле пользователя
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Рост в сантиметрах
        /// </summary>
        public short Height { get; set; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        public float Weight { get; set; }
    }
}
