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
    class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable(t => t.HasComment("Медикаменты"));

            builder.Property(m => m.Id)
                .HasComment("Идентификатор");

            builder.Property(m => m.Name)
               .HasComment("Наименование препарата")
               .HasMaxLength(150);

            builder.Property(m => m.DosageFormId)
               .HasComment("Форма выпуска (таблетка, капсул, раствор и т.д.)");

            builder.Property(m => m.Instruction)
               .HasComment("Инструкции по применению");

            builder.HasOne<DosageForm>(m => m.DosageForm)
                .WithMany()
                .HasForeignKey(m => m.DosageFormId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .Append(typeof(Medication).Name)
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
                            DosageFormId = csv.GetField<int>(2),
                            Instruction = csv.GetField(3)!.Trim(),
                    }
                    ;
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
