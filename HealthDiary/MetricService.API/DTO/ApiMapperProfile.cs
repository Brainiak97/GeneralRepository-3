using AutoMapper;
using MetricService.API.DTO.HealthCondition.Requests;
using MetricService.API.DTO.HealthCondition.Responses;
using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthCondition;
using MetricService.BLL.DTO.Reminder;

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
            CreateMap<ApiHealtConditionCreateRequest, HealthConditionCreateDTO>().ReverseMap();

            CreateMap<ApiHealthConditionUpdateRequestDTO, HealthConditionUpdateDTO>();

            CreateMap<Domain.Models.HealthCondition, ApiHealthConditionDTO>();

            CreateMap<ApiListWithPeriodByIdRequestDTO, RequestListWithPeriodByIdDTO>();

            CreateMap<ApiReminderCreateRequestDTO, ReminderCreateDTO>().ReverseMap();

            CreateMap<ApiReminderUpdateRequestDTO, ReminderUpdateDTO>();

            CreateMap<Domain.Models.Reminder, ApiReminderDTO>();

            CreateMap<ApiRequestListWithPeriodByRegimenIdDTO, RequestListWithPeriodByRegimenIdDTO>();
        }
    }
}
