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

        CreateMap<SaveAppointmentResultRequest, AppointmentResult>()
            .ForMember(x => x.Id, options => options.MapFrom(s => s.AppointmentSlotId));
        CreateMap<AppointmentResultDto, AppointmentResult>().ReverseMap();
    }
}