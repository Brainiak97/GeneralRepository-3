using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class AnalysisCategoryMapper
    {
        public static AnalysisCategoryCreateDTO ToAnalysisCategoryCreateDTO(this AnalysisCategory analysisCategory)
        {
            return new AnalysisCategoryCreateDTO
            {
                Description = analysisCategory.Description,
                Name = analysisCategory.Name,
            };
        }

        public static AnalysisCategory ToAnalysisCategory(this AnalysisCategoryCreateDTO analysisCategoryCreateDTO)
        {
            return new AnalysisCategory
            {
                Name = analysisCategoryCreateDTO.Name,
                Description = analysisCategoryCreateDTO.Description,
                Id = 0
            };
        }

        public static AnalysisCategoryUpdateDTO ToAnalysisCategoryUpdateDTO(this AnalysisCategory analysisCategory)
        {
            return new AnalysisCategoryUpdateDTO
            {
                Id = analysisCategory.Id,
                Description = analysisCategory.Description,
                Name = analysisCategory.Name,
            };
        }

        public static AnalysisCategory ToAnalysisCategory(this AnalysisCategoryUpdateDTO analysisCategoryUpdateDTO)
        {
            return new AnalysisCategory
            {
                Description = analysisCategoryUpdateDTO.Description,
                Name = analysisCategoryUpdateDTO.Name,
                Id = analysisCategoryUpdateDTO.Id
            };
        }

        public static AnalysisCategoryDTO ToAnalysisCategoryDTO(this AnalysisCategory analysisCategory)
        {
            return new AnalysisCategoryDTO
            {
               Description= analysisCategory.Description,
               Name = analysisCategory.Name,
               Id = analysisCategory.Id
            };
        }

        public static AnalysisCategory ToAnalysisCategory(this AnalysisCategoryDTO analysisCategoryDTO)
        {
            return new AnalysisCategory
            {
                Description=analysisCategoryDTO.Description,
                Name = analysisCategoryDTO.Name,
                Id = analysisCategoryDTO.Id
            };
        }

        public static IEnumerable<AnalysisCategoryDTO> ToAnalysisCategoryDTO(this IEnumerable<AnalysisCategory> analysisCategories)
        {
            var result = new List<AnalysisCategoryDTO>();

            foreach (var analysisCategory in analysisCategories)
            {
                result.Add(ToAnalysisCategoryDTO(analysisCategory));
            }
            return result;
        }
    }
}
