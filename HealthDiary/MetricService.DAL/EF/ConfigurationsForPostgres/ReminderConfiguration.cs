using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.ToTable(t => t.HasComment("Напоминание о приеме лекарств"));

            builder.Property(r => r.Id)
                .HasComment("Идентификатор");

            builder.Property(r => r.RegimenId)
               .HasComment("Схема приема лекарств");

            builder.Property(r => r.RemindAt)
               .HasComment("Время напоминания")
               .HasColumnType("timestamp with time zone");

            builder.Property(r => r.IsSend)
               .HasComment("Признак, было ли отправлено напоминание");

            builder.HasOne<Regimen>(r => r.Regimen)
                .WithMany()
                .HasForeignKey(r => r.RegimenId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
