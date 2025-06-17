using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class AnalysisResultsConfigeration : IEntityTypeConfiguration<AnalysisResult>
    {
        public void Configure(EntityTypeBuilder<AnalysisResult> builder)
        {
            builder.ToTable(t => t.HasComment("Результаты анализов"));

            builder.Property(a => a.Id)
                .HasComment("Идентификатор");

            builder.Property(a => a.UserId)
               .HasComment("Идентификатор пользователя");

            builder.Property(a => a.AnalysisTypeId)
               .HasComment("Тип анализа");

            builder.Property(a => a.Value)
               .HasComment("Числовое значение результата анализа");

            builder.Property(a => a.DetailedResearchDescription)
               .HasComment("Развернутое описание исследования");

            builder.Property(a => a.TestedAt)
              .HasComment("Дата сдачи анализа");

            builder.Property(a => a.Comment)
             .HasComment("Любые заметки или замечания по этому анализу");

            builder.HasOne<User>(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<AnalysisType>(a => a.AnalysisType)
               .WithMany()
               .HasForeignKey(a => a.AnalysisTypeId)
               .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
