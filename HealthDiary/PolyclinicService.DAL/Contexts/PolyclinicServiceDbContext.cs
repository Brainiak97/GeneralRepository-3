using Microsoft.EntityFrameworkCore;
using PolyclinicService.DAL.Infrastructure.Configurations;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Contexts;

/// <summary>
/// Контекст базы данных для сервиса поликлиник. 
/// </summary>
/// <remarks>
/// Инициализирует новый экземпляр контекста базы данных.
/// </remarks>
/// <param name="options">Настройки контекста базы данных.</param>
internal class PolyclinicServiceDbContext(DbContextOptions<PolyclinicServiceDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Возвращает или устанавливает набор сущностей <see cref="Polyclinic"/> в базе данных.
    /// </summary>
    public DbSet<Polyclinic> Polyclinics { get; set; }

    /// <summary>
    /// Возвращает или устанавливает набор сущностей <see cref="Doctor"/> в базе данных.
    /// </summary>
    public DbSet<Doctor> Doctors { get; set; }

    /// <summary>
    /// Возвращает или устанавливает набор сущностей <see cref="AppointmentSlot"/> в базе данных.
    /// </summary>
    public DbSet<AppointmentSlot> AppointmentSlots { get; set; }

    /// <summary>
    /// Возвращает или устанавливает набор сущностей <see cref="AppointmentResult"/> в базе данных.
    /// </summary>
    public DbSet<AppointmentResult> AppointmentResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("polyclinics")
            .ApplyConfiguration(new PolyclinicEntityTypeConfiguration())
            .ApplyConfiguration(new DoctorEntityTypeConfiguration())
            .ApplyConfiguration(new AppointmentSlotEntityTypeConfiguration())
            .ApplyConfiguration(new AppointmentResultEntityTypeConfiguration());

        modelBuilder.Entity<Polyclinic>()
            .HasMany(p => p.Doctors)
            .WithMany(d => d.Polyclinics)
            .UsingEntity<Dictionary<string, object>>(
                "polyclinic_doctors",
                j => j
                    .HasOne<Doctor>()
                    .WithMany()
                    .HasForeignKey("doctor_id"),
                j => j
                    .HasOne<Polyclinic>()
                    .WithMany()
                    .HasForeignKey("polyclinic_id"));
    }
}