using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class IntakeConfiguration : IEntityTypeConfiguration<Intake>
    {
        public void Configure(EntityTypeBuilder<Intake> builder)
        {
            builder.ToTable(t => t.HasComment("Прием лекарств"));

            builder.Property(i => i.Id)
                .HasComment("Идентификатор");

            builder.Property(i => i.RegimenId)
               .HasComment("Схема приема лекарств");

            builder.Property(i => i.TakenAt)
               .HasComment("Дата и время приема")
               .HasColumnType("timestamp with time zone");

            builder.Property(i => i.IntakeStatus)
               .HasComment("Статусы приема (например, \"принято\", \"пропущено\", \"перенесено\")")
               .HasColumnType("smallint");

            builder.Property(i => i.Comment)
                .HasComment("Дополнительные заметки (например, причины пропуска)");

            builder.HasOne<Regimen>(i => i.Regimen)
                .WithMany()
                .HasForeignKey(i => i.RegimenId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
