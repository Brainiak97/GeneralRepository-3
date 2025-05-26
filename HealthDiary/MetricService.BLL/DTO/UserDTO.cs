using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.BLL.Dto
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
        public Int16 Height { get; set; }

        /// <summary>
        /// Вес в килограммах
        /// </summary>
        public double Weight { get; set; }
    }
}
