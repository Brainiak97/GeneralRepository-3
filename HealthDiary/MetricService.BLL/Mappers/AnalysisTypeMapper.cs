using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.AnalysisType;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class AnalysisTypeMapper
    {
        public static AnalysisTypeCreateDTO ToAnalysisTypeCreateDTO(this AnalysisType analysisType)
        {
            return new AnalysisTypeCreateDTO
            {
                AnalysisCategoryId = analysisType.AnalysisCategoryId,
                ReferenceValueFemale = analysisType.ReferenceValueFemale,
                Name = analysisType.Name,
                ReferenceValueMale = analysisType.ReferenceValueMale,
                Unit = analysisType.Unit
            };
        }

        public static AnalysisType ToAnalysisType(this AnalysisTypeCreateDTO analysisTypeCreateDTO)
        {
            return new AnalysisType
            {
                AnalysisCategoryId = analysisTypeCreateDTO.AnalysisCategoryId,
                Id = 0,
                Name = analysisTypeCreateDTO.Name,
                ReferenceValueFemale = analysisTypeCreateDTO.ReferenceValueFemale,
                Unit = analysisTypeCreateDTO.Unit,
                ReferenceValueMale = analysisTypeCreateDTO.ReferenceValueMale,
            };
        }

        public static AnalysisTypeUpdateDTO ToAnalysisTypeUpdateDTO(this AnalysisType analysisType)
        {
            return new AnalysisTypeUpdateDTO
            {
                AnalysisCategoryId = analysisType.AnalysisCategoryId,
                Id = analysisType.Id,
                Name = analysisType.Name,
                ReferenceValueFemale = analysisType.ReferenceValueFemale,
                Unit = analysisType.Unit,
                ReferenceValueMale = analysisType.ReferenceValueMale,
            };
        }

        public static AnalysisType ToAnalysisType(this AnalysisTypeUpdateDTO analysisTypeUpdateDTO)
        {
            return new AnalysisType
            {
                AnalysisCategoryId = analysisTypeUpdateDTO.AnalysisCategoryId,
                Id = analysisTypeUpdateDTO.Id,
                Name = analysisTypeUpdateDTO.Name,
                ReferenceValueFemale = analysisTypeUpdateDTO.ReferenceValueFemale,
                Unit = analysisTypeUpdateDTO.Unit,
                ReferenceValueMale = analysisTypeUpdateDTO.ReferenceValueMale
            };
        }

        public static AnalysisTypeDTO ToAnalysisTypeDTO(this AnalysisType analysisType)
        {
            return new AnalysisTypeDTO
            {
                ReferenceValueMale = analysisType.ReferenceValueMale,
                Unit = analysisType.Unit,
                ReferenceValueFemale = analysisType.ReferenceValueFemale,
                Name = analysisType.Name,
                AnalysisCategoryId = (int)analysisType.AnalysisCategoryId,
                Id = analysisType.Id,
            };
        }

        public static AnalysisType ToAnalysisType(this AnalysisTypeDTO analysisTypeDTO)
        {
            return new AnalysisType
            {
                AnalysisCategoryId= analysisTypeDTO.AnalysisCategoryId,
                Id = analysisTypeDTO.Id,
                Name = analysisTypeDTO.Name,
                ReferenceValueFemale= analysisTypeDTO.ReferenceValueFemale,
                ReferenceValueMale= analysisTypeDTO.ReferenceValueMale,
                Unit= analysisTypeDTO.Unit,
            };
        }

        public static IEnumerable<AnalysisTypeDTO> ToAnalysisTypeDTO(this IEnumerable<AnalysisType> analysisTypes)
        {
            var result = new List<AnalysisTypeDTO>();

            foreach (var analysisType in analysisTypes)
            {
                result.Add(ToAnalysisTypeDTO(analysisType));
            }
            return result;
        }
    }
}
