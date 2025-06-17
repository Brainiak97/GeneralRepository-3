using CsvHelper.Configuration;
using CsvHelper;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class AnalysisTypesConfiguration : IEntityTypeConfiguration<AnalysisType>
    {
        public void Configure(EntityTypeBuilder<AnalysisType> builder)
        {
            builder.ToTable(t => t.HasComment("Типы анализов"));

            builder.Property(a => a.Id)
                .HasComment("Идентификатор");

            builder.Property(a => a.AnalysisCategoryId)
               .HasComment("Ссылка на категорию анализа");

            builder.Property(a => a.Name)
               .HasComment("Название конкретного анализа(например, «Лейкоциты», «Холестерин»)")
               .HasMaxLength(150); ;

            builder.Property(a => a.ReferenceValueMale)
               .HasComment("Эталонное значение мужской")
               .HasMaxLength(150); ;

            builder.Property(a => a.ReferenceValueFemale)
               .HasComment("Эталонное значение женский")
               .HasMaxLength(150); ;

            builder.HasOne<AnalysisCategory>(a => a.AnalysisCategory)
                .WithMany()
                .HasForeignKey(a => a.AnalysisCategoryId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasData(InitData());
        }

        private IEnumerable<object> InitData()
        {
            var sb = new StringBuilder();
            sb.Append(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .Append(Path.DirectorySeparatorChar)
                .Append("EF")
                .Append(Path.DirectorySeparatorChar)
                .Append("InitData")
                .Append(Path.DirectorySeparatorChar)
                .Append(typeof(AnalysisType).Name)
                .Append(".csv");

            var records = new List<object>();

            try
            {
                var readerConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
                readerConfiguration.Delimiter = ";";
                using (var reader = new StreamReader(sb.ToString()))
                using (var csv = new CsvReader(reader, readerConfiguration))
                {
                    var i = 0;
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        i++;
                        var record = new
                        {
                            Id = i,
                            AnalysisCategoryId = csv.GetField<int>(0),
                            Name = csv.GetField(1)!.Trim(),
                            Unit = csv.GetField(2)!.Trim(),
                            ReferenceValueMale = csv.GetField(3)!.Trim(),
                            ReferenceValueFemale = csv.GetField(4)!.Trim(),
                        };
                        records.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(0);
            }
            return records;
        }
    }
}
