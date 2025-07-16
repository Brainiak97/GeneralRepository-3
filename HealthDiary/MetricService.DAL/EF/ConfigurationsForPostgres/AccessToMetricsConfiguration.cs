using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class AccessToMetricsConfiguration : IEntityTypeConfiguration<AccessToMetrics>
    {
        public void Configure(EntityTypeBuilder<AccessToMetrics> builder)
        {
            builder.ToTable(t => t.HasComment("Доступ к личным метрикам"));

            builder.Property(a => a.Id)
                .HasComment("Идентификатор");

            builder.Property(a => a.ProviderUserId)
               .HasComment("Идентификатор пользователя, который предоставляет доступ");               

            builder.Property(a => a.GrantedUserId)
               .HasComment("Идентификатор пользователя, которому предоставляется доступ");

            builder.Property(a => a.AccessExpirationDate)
                .HasComment("Дата, до которой действует доступ")
                .HasColumnType("date"); ;

            builder.Property(a => a.IsPermanentAccess)
                .HasComment("Доступ без ограничения по срокам");

            builder.HasOne<User>(a => a.ProviderUser)
                .WithMany()
                .HasForeignKey(a => a.ProviderUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>(a => a.GrantedUser)
                .WithMany()
                .HasForeignKey(a => a.GrantedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
