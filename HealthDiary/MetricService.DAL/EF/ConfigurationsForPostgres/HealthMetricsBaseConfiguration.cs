using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class HealthMetricsBaseConfiguration : IEntityTypeConfiguration<HealthMetricsBase>
    {
        public void Configure(EntityTypeBuilder<HealthMetricsBase> builder)
        {
            builder.ToTable(t => t.HasComment("Базовые медицинские показатели"));

            builder.Property(p => p.Id)
                .HasComment("Идентификатор");

            builder.Property(p => p.UserId)
                .HasComment("Идентификатор пользователя");

            builder.Property(p => p.MetricDate)
               .HasComment("Дата замера показателя")
               .HasColumnType("timestamp with time zone");

            builder.Property(p => p.HeartRate)
              .HasComment("Частота сердечных сокращений (ударов/мин)");

            builder.Property(p => p.BloodPressureSys)
              .HasComment("Верхнее артериальное давление (мм рт. ст.)");

            builder.Property(p => p.BloodPressureDia)
              .HasComment("Нижнее артериальное давление (мм рт. ст.)");

            builder.Property(p => p.BodyFatPercentage)
              .HasComment("Процент жира в организме");

            builder.Property(p => p.WaterIntake)
              .HasComment("Потребление воды (мл)");

            builder.HasOne<User>(h => h.User)
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
