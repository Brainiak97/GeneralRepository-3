using AutoMapper;
using MetricService.API.DTO.HealthCondition.Requests;
using MetricService.API.DTO.HealthCondition.Responses;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthCondition;

namespace MetricService.API.DTO
{
    /// <summary>
    /// Профиль автомаппера в слое API
    /// </summary>
    public class ApiMapperProfile : Profile
    {
        /// <summary>
        /// Создает объект профиля автомаппера
        /// </summary>
        public ApiMapperProfile()
        {
            CreateMap<ApiHealtConditionCreateRequest, HealthConditionCreateDTO>()
                .ConstructUsing((source, _) =>
                    new HealthConditionCreateDTO(
                        UserId: source.UserId,
                        RecordedAt: source.RecordedAt,
                        EmotionalState: source.EmotionalState,
                        PhysicalState: source.PhysicalState,
                        Symptoms: source.Symptoms,
                        Notes: source.Notes
                        )
                )
                .ReverseMap();

            CreateMap<ApiHealthConditionUpdateRequestDTO, HealthConditionUpdateDTO>();                

            CreateMap<Domain.Models.HealthCondition, ApiHealthConditionDTO>()
                .ConstructUsing(source =>
                new ApiHealthConditionDTO(
                    source.Id,
                    source.UserId,
                    source.RecordedAt,
                    source.EmotionalState,
                    source.PhysicalState,
                    source.Symptoms,
                    source.Notes
                    )
                );
            CreateMap<ApiListWithPeriodByIdRequestDTO, RequestListWithPeriodByIdDTO>()
                .ConstructUsing((source, _) =>
                {
                    return new RequestListWithPeriodByIdDTO()
                    {
                        BegDate = source.BegDate,
                        EndDate = source.EndDate,
                        UserId = source.UserId,
                    };
                });
        }
    }
}
