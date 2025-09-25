using AutoMapper;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.BLL.Mappers;

/// <summary>
/// Профиль AutoMapper для сервиса поликлиник.
/// </summary>
public class PolyclinicServiceMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля AutoMapper для сервиса поликлиник.
    /// </summary>
    public PolyclinicServiceMapperProfile()
    {
        CreateMap<DoctorDto, Doctor>().ReverseMap();
        CreateMap<AddDoctorRequest, Doctor>();

        CreateMap<AddPolyclinicRequest, Polyclinic>();
        CreateMap<PolyclinicDto, Polyclinic>().ReverseMap();

        CreateMap<AddAppoinmentSlotRequest, AppointmentSlot>();
        CreateMap<AppointmentSlotDto, AppointmentSlot>().ReverseMap();

        CreateMap<SaveAppointmentResultRequest, AppointmentResult>();
        CreateMap<AppointmentResultDto, AppointmentResult>().ReverseMap();
        CreateMap<AppointmentResult, AppointmentResultExtDto>()
            .ForMember(d =>
                d.SlotInfo,
                options => options.MapFrom(s => s.AppointmentSlot));
    }
}