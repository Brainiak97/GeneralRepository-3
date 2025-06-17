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
    class AnalysisCategoriesConfiguration : IEntityTypeConfiguration<AnalysisCategory>
    {        public void Configure(EntityTypeBuilder<AnalysisCategory> builder)
        {
            builder.ToTable(t => t.HasComment("Категории анализов"));

            builder.Property(a => a.Id)
                .HasComment("Идентификатор");

            builder.Property(a => a.Name)
               .HasComment("Наименование категории анализа")
               .HasMaxLength(150); ;

            builder.Property(a => a.Description)
               .HasComment("Описание категории анализа");

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
                .Append(typeof(AnalysisCategory).Name)
                .Append(".csv");


            var records = new List<object>();

            try
            {
                var readerConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
                readerConfiguration.Delimiter = ";";
                using (var reader = new StreamReader(sb.ToString()))
                using (var csv = new CsvReader(reader, readerConfiguration))
                {

                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = new
                        {
                            Id = csv.GetField<int>(0),
                            Name = csv.GetField(1)!.Trim(),
                            Description = csv.GetField(2)!.Trim()
                        };
                        records.Add(record);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                Environment.Exit(0);
            }
            return records;
        }
    }
}
