using CsvHelper;
using CsvHelper.Configuration;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace MetricService.DAL.EF.ConfigurationsForPostgres
{
    class HealthMetricConfiguration : IEntityTypeConfiguration<HealthMetric>
    {
        public void Configure(EntityTypeBuilder<HealthMetric> builder)
        {
            builder.HasData();
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
                .Append(typeof(HealthMetric).Name)
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
                            Description = csv.GetField(2)!.Trim(),
                            Unit = csv.GetField(3)!.Trim()
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
