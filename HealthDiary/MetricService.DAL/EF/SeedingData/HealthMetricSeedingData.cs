using CsvHelper;
using CsvHelper.Configuration;
using MetricService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace MetricService.DAL.EF.SeedingData
{
    internal class HealthMetricSeedingData
    {
        private static IEnumerable<HealthMetric> InitData()
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

            var records = new List<HealthMetric>();

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
                        var record = new HealthMetric
                        {
                            Id = 0,
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
        static internal void SeedingData(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((dbContext, _) =>
            {
                if (!dbContext.Set<HealthMetric>().Any())
                {
                    dbContext.Set<HealthMetric>().AddRange(InitData());
                    dbContext.SaveChanges();
                }
            });

            optionsBuilder.UseAsyncSeeding(async (dbContext, _, cancellationToken) =>
            {
                if (!dbContext.Set<HealthMetric>().Any())
                {
                    await dbContext.Set<HealthMetric>().AddRangeAsync(InitData(), cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            });
        }
    }
}
