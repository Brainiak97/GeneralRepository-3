using AutoMapper;
using MetricServiceContracts=MetricService.Api.Contracts.Dtos.Common;
using StateServiceContracts = StateService.Api.Contracts.Dtos;
using StateServiceDomainDto = StateService.Domain.Dto;
using StateServiceDomain = StateService.Domain.Models;

namespace StateService.Api.Mapping
{
	public class ApiMapperProfile : Profile
	{
		public ApiMapperProfile()
		{
            CreateMap<StateServiceContracts.RequestListWithPeriodByIdDto, MetricServiceContracts.RequestListWithPeriodByIdDTO>();

            CreateMap<StateServiceDomain.UserHealthReport, StateServiceContracts.UserHealthReportDto>();

            CreateMap<StateServiceDomainDto.Sleep, StateServiceContracts.SleepDto>();

            CreateMap<StateServiceDomainDto.Workout, StateServiceContracts.WorkoutDto>();

            CreateMap<StateServiceDomainDto.AggregatedHealthSummary, StateServiceContracts.AggregatedHealthSummaryDto>().ReverseMap();

			CreateMap<StateServiceDomainDto.HealthMetrics, StateServiceContracts.HealthMetricsDto>();

			CreateMap<StateServiceContracts.RequestListWithPeriodByIdDto, StateServiceDomainDto.RequestListWithPeriodById>();

            CreateMap<StateServiceDomainDto.RequestListWithPeriodById, MetricServiceContracts.RequestListWithPeriodByIdDTO>();
        }
	}
}
