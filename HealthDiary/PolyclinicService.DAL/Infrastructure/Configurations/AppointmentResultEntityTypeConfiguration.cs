using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Infrastructure.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AppointmentResult"/> в базе данных.
/// </summary>
internal class AppointmentResultEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentResult> 
{
    public void Configure(EntityTypeBuilder<AppointmentResult> builder)
    {
        builder
            .ToTable(
                "appointment_results",
                t => t.HasComment("Результаты приёмов к врачам"))
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasComment("Идентификатор результата приёма к врачу")
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.ReportContent)
            .HasColumnName("report_content")
            .HasComment("Содержание отчёта приёма пациента")
            .IsRequired();
        
        builder.Property(x => x.ReportTemplateId)
            .HasColumnName("report_template_id")
            .HasComment("Идентификатор шаблона отчёта")
            .IsRequired();
        
        builder.Property(x => x.AppointmentSlotId)
            .HasColumnName("appointment_slot_id")
            .HasComment("Идентификатор слота на приём к врачу из графика")
            .IsRequired();

        builder
            .HasOne(r => r.AppointmentSlot)
            .WithOne()
            .HasForeignKey<AppointmentResult>(r => r.AppointmentSlotId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}