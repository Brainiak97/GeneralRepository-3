namespace StateService.Api.Contracts.Dtos
{
    public class RecomendationDto
    {
        public required string Period { get; set; }
        public required string Recomendation { get; set; }
        public required RecomendationSummaryDto Summary { get; set; }
    }
}
