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
internal class DoctorsService(
    IDoctorsRepository doctorsRepository,
    IServiceModelValidator serviceModelValidator,
    IMapper mapper)
    : IDoctorsService
{
    /// <inheritdoc />
    public async Task<int> AddAsync(AddDoctorRequest request)
    {
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var doctor = mapper.Map<Doctor>(request);
        return await doctorsRepository.AddAsync(doctor);
    }

    /// <inheritdoc />
    public async Task UpdateDoctorInfoAsync(UpdateDoctorRequest request)
    {
        await serviceModelValidator.ValidateAndThrowAsync(request);

        var doctor = await doctorsRepository.GetByIdAsync(request.DoctorId);
        if (doctor is null)
        {
            throw new EntryNotFoundException("Доктор не найден");
        }

        doctor.Seniority = request.Seniority ?? doctor.Seniority;
        doctor.QualificationType = request.QualificationType ?? doctor.QualificationType;
        doctor.AcademyDegree = request.AcademyDegree ?? doctor.AcademyDegree;
        doctor.IsConfirmedEducation = request.IsConfirmedEducation ?? doctor.IsConfirmedEducation;
        doctor.IsConfirmedQualification = request.IsConfirmedQualification ?? doctor.IsConfirmedQualification;
        doctor.SpecializationType = request.SpecializationType ?? doctor.SpecializationType;

        await doctorsRepository.UpdateAsync(doctor);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int doctorId) =>
        await doctorsRepository.DeleteAsync(doctorId);

    /// <inheritdoc />
    public async Task<DoctorDto?> GetById(int doctorId) =>
        mapper.Map<DoctorDto?>(await doctorsRepository.GetByIdAsync(doctorId));

    /// <inheritdoc />
    public async Task<DoctorDto[]> GetAll() =>
        (await doctorsRepository.GetAllAsync() ?? []).Select(mapper.Map<DoctorDto>).ToArray();

    /// <inheritdoc />
    public async Task<DoctorDto[]> GetPolyclinicDoctors(int polyclinicId) =>
        (await doctorsRepository.GetByPolyclinicId(polyclinicId) ?? []).Select(mapper.Map<DoctorDto>).ToArray();
}