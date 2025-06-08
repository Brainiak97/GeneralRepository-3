using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class SleepsConfiguration : IEntityTypeConfiguration<Sleep>
    {
        public void Configure(EntityTypeBuilder<Sleep> builder)
        {
            builder.ToTable(t => t.HasComment("Сон")
                            .HasCheckConstraint("ValidQualityRating", "\"QualityRating\">=1 and \"QualityRating\"<=5")
                            );

            builder.Property(p => p.Id)
                .HasComment("Идентификатор");

            builder.Property(p => p.UserId)
               .HasComment("идентификатор пользователя");

            builder.Property(p => p.StartSleep)
               .HasComment("время начала сна")
               .HasColumnType("timestamp with time zone");

            builder.Property(p => p.EndSleep)
               .HasComment("время окончания сна")
               .HasColumnType("timestamp with time zone");

            builder.Property(p => p.QualityRating)
               .HasComment("качество сна по 5-ой системе");

            builder.Property(p => p.Comment)
              .HasComment("примечания о качестве сна");

            builder.HasOne<User>(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
