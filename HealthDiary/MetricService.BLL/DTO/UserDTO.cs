namespace MetricService.BLL.DTO
{
    public  class UserDTO
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// рост в сантиметрах
        /// </summary>
        public short Height { get; set; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public short Age { get; internal set; }
    }
}
