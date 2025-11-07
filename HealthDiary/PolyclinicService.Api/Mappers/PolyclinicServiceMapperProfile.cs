using AutoMapper;
using PolyclinicService.Api.Contracts.Data.Enums;
using PolyclinicService.Api.Contracts.Data.Requests;
using PolyclinicService.BLL.Data.Commands;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.Domain.Models.Entities;
using BllRequests = PolyclinicService.BLL.Data.Requests;

namespace PolyclinicService.Api.Mappers;

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
        CreateMap<BllRequests.AddDoctorRequest, Doctor>();

        CreateMap<BllRequests.AddPolyclinicRequest, Polyclinic>();
        CreateMap<PolyclinicDto, Polyclinic>().ReverseMap();

        CreateMap<AppointmentSlotStatus, Domain.Models.AppointmentSlotStatus>().ReverseMap();
        
        CreateMap<AddAppoinmentSlotRequest, AddAppoinmentSlotCommand>();
        CreateMap<AddAppointmentSlotsByTemplateRequest, AddAppointmentSlotsByTemplateCommand>();
        CreateMap<UpdateAppointmentSlotRequest, UpdateAppointmentSlotCommand>();
        CreateMap<UpdateAppointmentSlotStatusRequest, UpdateAppointmentSlotStatusCommand>();
        CreateMap<PolyclinicAppointmentSlotsByDateRequest, PolyclinicAppointmentSlotsByDateCommand>();
        CreateMap<DoctorActiveAppointmentSlotsRequest, DoctorActiveAppointmentSlotsCommand>();
        CreateMap<DeletePolyclinicAppointmentSlotsByFilterRequest, DeletePolyclinicAppointmentSlotsByFilterCommand>();
        CreateMap<UserSlotReservationRequest, UserSlotReservationCommand>();

        CreateMap<AddAppoinmentSlotCommand, AppointmentSlot>();
        CreateMap<UpdateAppointmentSlotCommand, AppointmentSlot>();
        
        CreateMap<AppointmentSlotDto, AppointmentSlot>().ReverseMap();

        CreateMap<BllRequests.SaveAppointmentResultRequest, AppointmentResult>()
            .ForMember(dest => dest.AppointmentSlot, opt => opt.Ignore());
        CreateMap<AppointmentResultDto, AppointmentResult>().ReverseMap();
    }
}