using AutoMapper;
using PolyclinicService.BLL.Common.ServiceModelsValidator.Interfaces;
using PolyclinicService.BLL.Data.Dtos;
using PolyclinicService.BLL.Data.Requests;
using PolyclinicService.BLL.Interfaces;
using PolyclinicService.DAL.Interfaces;
using PolyclinicService.Domain.Models.Entities;
using Shared.Common.Exceptions;

namespace PolyclinicService.BLL.Services;

/// <inheritdoc />
internal class PolyclinicService(
    IPolyclinicsRepository polyclinicsRepository,
    IServiceModelValidator serviceModelValidator,
    IMapper mapper)
    : IPolyclinicsService
{
    /// <inheritdoc />
    public async Task<int> AddPolyclinicAsync(AddPolyclinicRequest request)
    {
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var polyclinic = mapper.Map<Polyclinic>(request);
        return await polyclinicsRepository.AddAsync(polyclinic);
    }

    /// <inheritdoc />
    public async Task UpdatePolyclinicInfoAsync(UpdatePolyclinicRequest request)
    {
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var polyclinic = await polyclinicsRepository.GetByIdAsync(request.PolyclinicId);
        if (polyclinic is null)
        {
            throw new EntryNotFoundException($"Не найдена поликлиника по идентификатору: {request.PolyclinicId}");
        }

        polyclinic.Name = request.Name ?? polyclinic.Name;
        polyclinic.Address = request.Address ?? polyclinic.Address;
        polyclinic.PhoneNumber = request.PhoneNumber ?? polyclinic.PhoneNumber;
        polyclinic.Email = request.Email ?? polyclinic.Email;
        polyclinic.Url = request.Url ?? polyclinic.Url;

        await polyclinicsRepository.UpdateAsync(polyclinic);
    }

    /// <inheritdoc />
    public async Task DeletePolyclinicAsync(int polyclinicId) =>
        await polyclinicsRepository.DeleteAsync(polyclinicId);

    /// <inheritdoc />
    public async Task<PolyclinicDto?> GetPolyclinicById(int polyclinicId) =>
        mapper.Map<PolyclinicDto?>(await polyclinicsRepository.GetByIdAsync(polyclinicId));

    /// <inheritdoc />
    public async Task<PolyclinicDto[]> GetAllPolyclinics() =>
       (await polyclinicsRepository.GetAllAsync())?.Select(mapper.Map<PolyclinicDto>).ToArray() ?? [];
}