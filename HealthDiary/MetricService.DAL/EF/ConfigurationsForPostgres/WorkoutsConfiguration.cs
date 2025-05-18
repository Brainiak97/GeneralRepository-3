using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class WorkoutsConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable(t => t.HasComment("Тренировки"));

            builder.Property(p => p.Id)
                .HasComment("Идентификатор");

            builder.Property(p => p.UserId)
                .HasComment("Идентификатор пользователя");

            builder.Property(p => p.PhysicalActivityId)
                .HasComment("Физ. активность");

            builder.HasOne<PhysicalActivity>(w=>w.PhysicalActivity)
                .WithMany()
                .HasForeignKey(w=>w.PhysicalActivityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.StartTime)
                .HasComment("Время начала тренировки")
                .HasColumnType("timestamp without time zone");

            builder.Property(p => p.EndTime)
                .HasComment("Время окончания тренировки")
                .HasColumnType("timestamp without time zone");            

            builder.Property(p => p.Description)
                .HasComment("Описание");

            builder.HasOne<User>(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
