using AutoMapper;
using MetricServiceContracts= MetricService.Api.Contracts.Dtos.Common;
using MetricService.Api.Contracts.Dtos.HealthMetricValue;
using MetricService.Api.Contracts.Dtos.Sleep;
using MetricService.Api.Contracts.Dtos.Workout;
using StateServiceContracts=StateService.Api.Contracts.Dtos;
using StateServiceDomainDto=StateService.Domain.Dto;

namespace StateService.BLL.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StateServiceContracts.RequestListWithPeriodByIdDto, MetricServiceContracts.RequestListWithPeriodByIdDTO>();

            CreateMap<HealthMetricValueDTO, StateServiceDomainDto.HealthMetrics>()
                .ForMember(dest => dest.MetricDate, opt => opt.MapFrom(src => src.RecordedAt))
                .ForMember(dest => dest.MetricName, opt => opt.MapFrom(src => src.HealthMetric.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.HealthMetric.Unit))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ReverseMap();

            CreateMap<WorkoutDTO, StateServiceDomainDto.Workout>()
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.CaloriesBurned, opt => opt.MapFrom(src => src.CaloriesBurned))
                .ForMember(dest => dest.PhysicalActivityId, opt => opt.MapFrom(src => src.PhysicalActivityId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

            CreateMap<SleepDTO, StateServiceDomainDto.Sleep>()
                .ForMember(dest => dest.EndSleep, opt => opt.MapFrom(src => src.EndSleep))
                .ForMember(dest => dest.StartSleep, opt => opt.MapFrom(src => src.StartSleep))
                .ForMember(dest => dest.QualityRating, opt => opt.MapFrom(src => src.QualityRating))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.SleepDuration, opt => opt.MapFrom(src => src.SleepDuration))
                .ReverseMap();
        }
    }
}
