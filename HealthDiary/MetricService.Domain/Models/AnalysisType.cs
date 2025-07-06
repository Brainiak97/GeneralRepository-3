namespace MetricService.Domain.Models
{

    /// <summary>
    /// Типы анализов
    /// </summary>
    public class AnalysisType : BaseModel 
    {
        /// <summary>
        /// Идентификатор категории анализа
        /// </summary>
        public int AnalysisCategoryId { get; set; }

        /// <summary>
        /// Категория анализа
        /// </summary>
        public AnalysisCategory AnalysisCategory { get; set; } = null!;

        /// <summary>
        /// Название конкретного анализа(например, «Лейкоциты», «Холестерин»)
        /// </summary>
        public string Name { get; set; } = string.Empty;


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
       public string? Unit {  get; set; }
    }
}
