using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Infrastructure.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AppointmentSlot"/> в базе данных.
/// </summary>
internal class AppointmentSlotEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentSlot>
{
    public void Configure(EntityTypeBuilder<AppointmentSlot> builder)
    {
        builder
            .ToTable(
                "appointment_slots",
                t => t.HasComment("Данные о приёмах к врачу в графиках поликлиники"))
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasComment("Идентификатор приёма в графике")
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.DoctorId)
            .HasColumnName("doctor_id")
            .HasComment("Идентификатор врача");

        builder.Property(x => x.PolyclinicId)
            .HasColumnName("polyclinic_id")
            .HasComment("Идентификатор поликлиники");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasComment("Идентификатор записанного пациента");
        
        builder.Property(x => x.Date)
            .HasColumnName("date")
            .HasComment("Дата приёма");
        
        builder.Property(x => x.StartTime)
            .HasColumnName("start_time")
            .HasComment("Время начала приёма");
        
        builder.Property(x => x.EndTime)
            .HasColumnName("end_time")
            .HasComment("Время окончания приёма");
        
        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasComment("Статус приёма в графике")
            .HasConversion<short>();
        
        builder.HasOne<Polyclinic>()
            .WithMany()
            .HasForeignKey(x => x.PolyclinicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}