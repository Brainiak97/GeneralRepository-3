﻿namespace MetricService.BLL.DTO.Regimen
{
    /// <summary>
    /// Объект для регистрации данных о схеме приема медикаментов пользователем
    /// </summary>
    public class RegimenCreateDTO: RegimenBaseDTO
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>       
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор данных из справочника "Медикаменты"
        /// </summary>       
        public int MedicationId { get; set; }
    }
}
