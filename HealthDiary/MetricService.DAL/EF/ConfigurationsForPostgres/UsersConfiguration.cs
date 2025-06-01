using Microsoft.EntityFrameworkCore;
using MetricService.Domain.Models;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable(t => t.HasComment("Пользователь"));

            builder.Property(p => p.Id)
                .HasComment("Идентификатор")
                .ValueGeneratedNever();                

            builder.Property(p => p.DateOfBirth)
                .HasComment("дата рождения")
                .HasColumnType("date");

            builder.Property(p => p.Height)
               .HasComment("рост в сантиметрах");
               

            builder.Property(p => p.Weight)
              .HasComment("Вес в килограммах");            
        }
    }
}
