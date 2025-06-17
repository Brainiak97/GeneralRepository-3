using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class AnalysisResultMapper
    {
        public static AnalysisResultCreateDTO ToAnalysisResultCreateDTO(this AnalysisResult analysisResult)
        {
            return new AnalysisResultCreateDTO
            {
                AnalysisTypeId = analysisResult.AnalysisTypeId,
                Comment = analysisResult.Comment,
                DetailedResearchDescription = analysisResult.DetailedResearchDescription,
                TestedAt = analysisResult.TestedAt,
                UserId = analysisResult.UserId,
                Value = analysisResult.Value,
            };
        }

        public static AnalysisResult ToAnalysisResult(this AnalysisResultCreateDTO analysisResultCreateDTO)
        {
            return new AnalysisResult
            {
                AnalysisTypeId = analysisResultCreateDTO.AnalysisTypeId,
                Comment = analysisResultCreateDTO.Comment,
                DetailedResearchDescription = analysisResultCreateDTO.DetailedResearchDescription,
                Value = analysisResultCreateDTO.Value,
                UserId = analysisResultCreateDTO.UserId,
                TestedAt = analysisResultCreateDTO.TestedAt,
                Id = 0
            };
        }

        public static AnalysisResultUpdateDTO ToAnalysisResultUpdateDTO(this AnalysisResult analysisResult)
        {
            return new AnalysisResultUpdateDTO
            {
                Id = analysisResult.Id,
                AnalysisTypeId = analysisResult.AnalysisTypeId,
                Comment = analysisResult.Comment,
                Value = analysisResult.Value,
                DetailedResearchDescription = analysisResult.DetailedResearchDescription,
                TestedAt = analysisResult.TestedAt,
            };
        }

        public static AnalysisResult ToAnalysisResult(this AnalysisResultUpdateDTO analysisResultUpdateDTO, int userId)
        {
            return new AnalysisResult
            {
                AnalysisTypeId = analysisResultUpdateDTO.AnalysisTypeId,
                Comment = analysisResultUpdateDTO.Comment,
                Value = analysisResultUpdateDTO.Value,
                TestedAt = analysisResultUpdateDTO.TestedAt,
                DetailedResearchDescription = analysisResultUpdateDTO.DetailedResearchDescription,
                Id = analysisResultUpdateDTO.Id,
                UserId=userId
            };
        }

        public static AnalysisResultDTO ToAnalysisReaultDTO(this AnalysisResult analysisResult)
        {
            return new AnalysisResultDTO
            {
                AnalysisTypeId = analysisResult.AnalysisTypeId,
                Comment = analysisResult.Comment,
                Value = analysisResult.Value,
                UserId = analysisResult.UserId,
                Id = analysisResult.Id,
                DetailedResearchDescription = analysisResult.DetailedResearchDescription,
                TestedAt = analysisResult.TestedAt,
            };
        }

        public static AnalysisResult ToAnalysisResult(this AnalysisResultDTO analysisResultDTO)
        {
            return new AnalysisResult
            {
               AnalysisTypeId= analysisResultDTO.AnalysisTypeId,
               Comment = analysisResultDTO.Comment,
               Value = analysisResultDTO.Value,
               UserId = analysisResultDTO.UserId,
               Id = analysisResultDTO.Id,
               TestedAt= analysisResultDTO.TestedAt,
               DetailedResearchDescription= analysisResultDTO.DetailedResearchDescription,               
            };
        }

        public static IEnumerable<AnalysisResultDTO> ToAnalysisResultDTO(this IEnumerable<AnalysisResult> analysisResults)
        {
            var result = new List<AnalysisResultDTO>();

            foreach (var analysisResult in analysisResults)
            {
                result.Add(ToAnalysisReaultDTO(analysisResult));
            }
            return result;
        }
    }
}
