using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Infrastructure.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="Doctor"/> в базе данных.
/// </summary>
internal class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder
            .ToTable(
                "doctors",
                t => t.HasComment("Врачи поликлиник"))
            .HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasComment("Идентификатор врача");
        
        builder.Property(p => p.Seniority)
            .HasComment("Стаж врача")
            .HasColumnName("seniority");
        
        builder.Property(p => p.QualificationType)
            .HasComment("Квалификация врача")
            .HasColumnName("qualification_type")
            .HasConversion<byte>();
        
        builder.Property(p => p.AcademyDegree)
            .HasComment("Научная степень врача")
            .HasColumnName("academy_degree")
            .HasConversion<byte>();
        
        builder.Property(p => p.SpecializationType)
            .HasComment("Специализация врача")
            .HasColumnName("specialization_type")
            .HasConversion<short>();
        
        builder.Property(p => p.IsConfirmedEducation)
            .HasComment("Признак, что у врача подтвержден документ об образовании")
            .HasColumnName("is_confirmed_education");
        
        builder.Property(p => p.IsConfirmedQualification)
            .HasComment("Признак, что у врача подтвержден документ о квалификации")
            .HasColumnName("is_confirmed_qualification");
    }
}