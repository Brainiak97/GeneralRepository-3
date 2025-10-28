namespace MetricService.Api.Contracts.Dtos.AnalysisType
{
    /// <summary>
    /// Объект базовых данных в справочнике "Типы анализов"
    /// </summary>
    public abstract record AnalysisTypeBaseDTO
    {
        /// <summary>
        /// Идентификатор данных из справочника "Категории анализов"
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
