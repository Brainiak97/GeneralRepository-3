using AutoMapper;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Mapper;

/// <summary>
/// Профиль AutoMapper для слоя DAL сервиса поликлиник.
/// </summary>
public class PolyclinicServiceDalMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля AutoMapper для слоя DAL сервиса поликлиник.
    /// </summary>
    public PolyclinicServiceDalMapperProfile()
    {
        CreateMap<AppointmentSlot, AppointmentSlot>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}