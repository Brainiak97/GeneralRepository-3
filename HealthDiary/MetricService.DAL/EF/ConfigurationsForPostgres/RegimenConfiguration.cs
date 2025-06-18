using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class RegimenConfiguration : IEntityTypeConfiguration<Regimen>
    {
        public void Configure(EntityTypeBuilder<Regimen> builder)
        {
            builder.ToTable(t => t.HasComment("Схема приема медикаментов"));

            builder.Property(r => r.Id)
                .HasComment("Идентификатор");

            builder.Property(r => r.UserId)
               .HasComment("Пользователь");

            builder.Property(r => r.MedicationId)
               .HasComment("Медицинский препарат");

            builder.Property(r => r.Dosage)
               .HasComment("Прописанная дозировка (например, \"1 табл.\" или \"5 мл\")");

            builder.Property(r => r.Shedule)
                .HasComment("График приема (например, \"Утро, обед, вечер\")");

            builder.Property(r => r.StartDate)
                .HasComment("Дата начала приема")
                .HasColumnType("date");

            builder.Property(r => r.EndDate)
               .HasComment("Предполагаемая дата окончания приема")
               .HasColumnType("date");

            builder.Property(r => r.Comment)
              .HasComment("Заметки или дополнения");

            builder.HasOne<User>(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Medication>(r => r.Medication)
                .WithMany()
                .HasForeignKey(r => r.MedicationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
