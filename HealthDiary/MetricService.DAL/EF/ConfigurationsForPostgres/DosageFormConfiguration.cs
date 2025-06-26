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
    class DosageFormConfiguration : IEntityTypeConfiguration<DosageForm>
    {
        public void Configure(EntityTypeBuilder<DosageForm> builder)
        {
            builder.ToTable(t => t.HasComment("Форма выпуска препарата"));

            builder.Property(d => d.Id)
                .HasComment("Идентификатор");

            builder.Property(d => d.Name)
               .HasComment("Наименование формы выпуска (таблетка, капсул, раствор и т.д.)")
               .HasMaxLength(150);
           
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
                .Append(typeof(DosageForm).Name)
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
                            Name = csv.GetField(1)!.Trim()                            
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
