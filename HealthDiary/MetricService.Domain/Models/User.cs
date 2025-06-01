namespace MetricService.Domain.Models
{
    public class User:BaseModel
    {
        /// <summary>
        /// дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// рост в сантиметрах
        /// </summary>
        public Int16 Height { get; set; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// возраст
        /// </summary>
        /// <returns>Значение возраста (лет)</returns>
        public Int16 Age
        {
            get
            {
                return (Int16)(( DateTime.Now - DateOfBirth).TotalDays / 365.25);
            }
        }

    }
}
