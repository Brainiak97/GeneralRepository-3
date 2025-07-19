using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<Polyclinic>(entity =>
        {
            entity.Property(t => t.Id).UseIdentityAlwaysColumn();
                
            entity.HasMany(p => p.Doctors)
                .WithMany(d => d.Polyclinics)
                .UsingEntity<Dictionary<string, object>>(
                    "PolyclinicDoctors",
                    j => j
                        .HasOne<Doctor>()
                        .WithMany()
                        .HasForeignKey("DoctorId"),
                    j => j
                        .HasOne<Polyclinic>()
                        .WithMany()
                        .HasForeignKey("PolyclinicId"));
        });

        modelBuilder.Entity<AppointmentSlot>(entity => entity.Property(x => x.Id).UseIdentityAlwaysColumn());
        modelBuilder.Entity<AppointmentResult>(entity => entity.Property(x => x.Id).UseIdentityAlwaysColumn());
    }
}