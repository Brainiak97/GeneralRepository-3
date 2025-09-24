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
        CreateMap<DoctorDto?, Doctor?>().ReverseMap()
            .ConvertUsing((source, destination) => source is null ? null : destination);
        CreateMap<DoctorDto, Doctor>().ReverseMap();
        CreateMap<AddDoctorRequest, Doctor>();

        CreateMap<PolyclinicDto?, Polyclinic?>()
            .ConvertUsing((source, destination) => source is null ? null : destination);
        CreateMap<AddPolyclinicRequest, Polyclinic>();
        CreateMap<PolyclinicDto, Polyclinic>().ReverseMap();

        CreateMap<AddAppoinmentSlotRequest, AppointmentSlot>();
        CreateMap<AppointmentSlotDto, AppointmentSlot>().ReverseMap();
        CreateMap<AppointmentSlotDto?, AppointmentSlot?>().ReverseMap()
            .ConvertUsing((source, destination) => source is null ? null : destination);
        CreateMap<SaveAppointmentResultRequest, AppointmentResult>();
        CreateMap<AppointmentResultDto, AppointmentResult>().ReverseMap();
        CreateMap<AppointmentResultDto?, AppointmentResult?>().ReverseMap()
            .ConvertUsing((source, destination) => source is null ? null : destination);
    }
}