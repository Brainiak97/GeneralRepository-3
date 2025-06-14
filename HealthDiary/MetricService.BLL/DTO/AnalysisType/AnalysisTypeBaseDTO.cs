namespace MetricService.BLL.DTO.AnalysisType
{
    public class AnalysisTypeBaseDTO
    {
        /// <summary>
        /// Ссылка на категорию анализа
        /// </summary>
        public int AnalysisCategoryId { get; set; }


        /// <summary>
        /// Название конкретного анализа(например, «Лейкоциты», «Холестерин»)
        /// </summary>
        public string Name { get; set; } = null!;


        /// <summary>
        ///Эталонное значение мужской
        /// </summary>        
        public string? ReferenceValueMale { get; set; }

        /// <summary>
        ///Эталонное значение женский
        /// </summary>        
        public string? ReferenceValueFemale { get; set; }


        /// <summary>
        /// Единица измерения(например, г/л, ммоль/л)
        /// </summary>        
        public string? Unit { get; set; }
    }
}
