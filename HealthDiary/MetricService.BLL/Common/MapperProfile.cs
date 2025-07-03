using AutoMapper;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.AnalysisCategory;
using MetricService.BLL.DTO.AnalysisResult;
using MetricService.BLL.DTO.AnalysisType;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.DTO.PhysicalActivity;
using MetricService.BLL.DTO.Sleep;
using MetricService.BLL.DTO.Workout;
using MetricService.Domain.Models;

namespace MetricService.BLL.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Workout, WorkoutCreateDTO>().ReverseMap();
            CreateMap<Workout, WorkoutUpdateDTO>().ReverseMap();
            CreateMap<Workout, WorkoutDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Sleep, SleepCreateDTO>().ReverseMap();
            CreateMap<Sleep, SleepUpdateDTO>().ReverseMap();
            CreateMap<Sleep, SleepDTO>().ReverseMap();

            CreateMap<PhysicalActivity, PhysicalActivityCreateDTO>().ReverseMap();
            CreateMap<PhysicalActivity, PhysicalActivityUpdateDTO>().ReverseMap();
            CreateMap<PhysicalActivity, PhysicalActivityDTO>().ReverseMap();

            CreateMap<HealthMetricsBase, HealthMetricsBaseCreateDTO>().ReverseMap();
            CreateMap<HealthMetricsBase, HealthMetricsBaseUpdateDTO>().ReverseMap();
            CreateMap<HealthMetricsBase, HealthMetricsBaseDTO>().ReverseMap();

            CreateMap<AnalysisCategory, AnalysisCategoryCreateDTO>().ReverseMap();
            CreateMap<AnalysisCategory, AnalysisCategoryUpdateDTO>().ReverseMap();
            CreateMap<AnalysisCategory, AnalysisCategoryDTO>().ReverseMap();

            CreateMap<AnalysisType, AnalysisTypeCreateDTO>().ReverseMap();
            CreateMap<AnalysisType, AnalysisTypeUpdateDTO>().ReverseMap();
            CreateMap<AnalysisType, AnalysisTypeDTO>().ReverseMap();

            CreateMap<AnalysisResult, AnalysisResultCreateDTO>().ReverseMap();
            CreateMap<AnalysisResult, AnalysisResultUpdateDTO>().ReverseMap();
            CreateMap<AnalysisResult, AnalysisResultDTO>().ReverseMap();            
        }
    }
}
