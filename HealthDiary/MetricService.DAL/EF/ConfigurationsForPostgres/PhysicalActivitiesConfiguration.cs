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
    internal class PhysicalActivitiesConfiguration : IEntityTypeConfiguration<PhysicalActivity>
    {
        public void Configure(EntityTypeBuilder<PhysicalActivity> builder)
        {
            builder.ToTable(t => t.HasComment("Тренировки"));

            builder.Property(p => p.Id)
                .HasComment("Идентификатор");

            builder.Property(p => p.Name)
               .HasComment("Наименование физической активности")
               .HasMaxLength(150);

            builder.Property(p => p.EnergyEquivalent)
               .HasComment("Метаболический эквивалент");

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
                .Append(nameof(PhysicalActivity))
                .Append(".csv");


            var records = new List<object>();

            var readerConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            readerConfiguration.Delimiter = ";";
            using (var reader = new StreamReader(sb.ToString()))
            using (var csv = new CsvReader(reader, readerConfiguration))
            {

                csv.Read();
                csv.ReadHeader();
                int i = 0;
                while (csv.Read())
                {
                    i++;
                    var record = new 
                    {
                        Id = i,
                        Name = csv.GetField(0)!.Trim(),
                        EnergyEquivalent = csv.GetField<float>(1)
                    };
                    records.Add(record);
                }
            }
            return records;
        }
    }
}
