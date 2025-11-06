using AutoMapper;
using MetricService.Api.Contracts.Dtos.DosageForm;
using MetricService.Api.Contracts.Dtos.HealthMetricValue;
using MetricService.Api.Contracts.Dtos.Intake;
using MetricService.Api.Contracts.Dtos.Medication;
using MetricService.Api.Contracts.Dtos.Regimen;
using MetricService.Api.Contracts.Dtos.Sleep;
using MetricService.Api.Contracts.Dtos.Workout;
using StateServiceDomainDto = StateService.Domain.Dto;

namespace StateService.BLL.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
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

            CreateMap<DosageFormDTO, StateServiceDomainDto.DosageForm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<IntakeDTO, StateServiceDomainDto.Intake>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.IntakeStatus, opt => opt.MapFrom(src => src.IntakeStatus))
                .ForMember(dest => dest.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dest => dest.RegimenId, opt => opt.MapFrom(src => src.RegimenId));

            CreateMap<MedicationDTO, StateServiceDomainDto.Medication>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DosageFormId, opt => opt.MapFrom(src => src.DosageFormId))
                .ForMember(dest => dest.DosageForm, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var mappings = (List<DosageFormDTO>)context.Items["DosageForm"];

                    return mappings.FirstOrDefault(n => n.Id == src.DosageFormId);
                }))
                .ForMember(dest => dest.Instruction, opt => opt.MapFrom(src => src.Instruction));

            CreateMap<RegimenDTO, StateServiceDomainDto.RegimenProgress>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MedicationId, opt => opt.MapFrom(src => src.MedicationId))
                .ForMember(dest => dest.Dosage, opt => opt.MapFrom(src => src.Dosage))
                .ForMember(dest => dest.Shedule, opt => opt.MapFrom(src => src.Shedule))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Medication, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var mappings = (List<MedicationDTO>)context.Items["Medications"];

                    return mappings.FirstOrDefault(m => m.Id == src.MedicationId);
                }))
                .ForMember(dest => dest.Intakes, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var mappings = (List<IntakeDTO>)context.Items["Intakes"];

                    return mappings.Where(i => i.RegimenId == src.Id);
                }));
        }
    }
}
