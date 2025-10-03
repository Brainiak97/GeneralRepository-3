using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReportService.Api.Contracts.Data.Dto;

namespace ReportService.BLL.Reports.Pdf.QuestPdfReports;

/// <summary>
/// Отчёт приёма у кардиолога.
/// </summary>
public class CardiologistReportTemplate : ReportTemplateBase<CardiologistReportDataDto>
{
    /// <inheritdoc />
    protected override void Compose(IDocumentContainer container, CardiologistReportDataDto data)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.DefaultTextStyle(TextStyle.Default.FontSize(11));

            page.Header().Element(e => ComposeHeader(e, 16));
            page.Content().Element(e => ComposeContent(e, data));
            page.Footer().AlignCenter().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
        });
    }

    private void ComposeContent(IContainer container, CardiologistReportDataDto reportData)
    {
        container.Column(column =>
        {
            column.Item().PaddingVertical(10);

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .Text($"{PropertyDisplayNames[nameof(reportData.DoctorFullName)]}: {reportData.DoctorFullName}")
                    .FontSize(11)
                    .AlignLeft();

                row.RelativeItem()
                    .Text($"{PropertyDisplayNames[nameof(reportData.AppointmentDate)]}: {reportData.AppointmentDate}")
                    .FontSize(11)
                    .AlignRight();
            });


            column.Item().PaddingVertical(10);

            column.Item().Column(contentColumn =>
            {
                contentColumn.Spacing(10);

                contentColumn.Item().Component(new PatientBaseInfoSection(reportData, PropertyDisplayNames));
                contentColumn.Item().Component(new AnamnesisSection(reportData, PropertyDisplayNames));
                contentColumn.Item().Component(new ExaminationSection(reportData, PropertyDisplayNames));
                contentColumn.Item().Component(new DiagnosisSection(reportData, PropertyDisplayNames));
            });
        });
    }

    private class PatientBaseInfoSection(
        CardiologistReportDataDto data,
        Dictionary<string, string> propertyNames) : IComponent
    {
        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(15)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(3);
                    });

                    table.Cell().Text($"{propertyNames[nameof(data.PatientFullName)]}: ").SemiBold().FontSize(11);
                    table.Cell().Text(data.PatientFullName).FontSize(11);

                    table.Cell().Text($"{propertyNames[nameof(data.PatientBirthDate)]}: ").SemiBold().FontSize(11);
                    table.Cell().Text(data.PatientBirthDate.ToString("dd.MM.yyyy")).FontSize(11);

                    table.Cell().Text($"{propertyNames[nameof(data.PatientWeight)]}: ").SemiBold().FontSize(11);
                    table.Cell().Text($"{data.PatientWeight.ToString("N2", CultureInfo.InvariantCulture)}").FontSize(11);

                    table.Cell().Text($"{propertyNames[nameof(data.PatientHeight)]}: ").SemiBold().FontSize(11);
                    table.Cell().Text($"{data.PatientHeight}").FontSize(11);

                    table.Cell().Text($"{propertyNames[nameof(data.Bmi)]}: ").SemiBold().FontSize(11);
                    table.Cell().Text($"{data.Bmi:F1}").FontSize(11);

                    table.Cell().Text($"{propertyNames[nameof(data.IsSmoker)]}: ").SemiBold().FontSize(11);
                    table.Cell().Text(data.IsSmoker ? "Курит" : "Не курит").FontSize(11);
                });
        }
    }

    private class AnamnesisSection(
        CardiologistReportDataDto data,
        Dictionary<string, string> propertyNames) : IComponent
    {
        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(10)
                .Column(column =>
                {
                    column.Item().Text("Данные от пациента").Bold().FontSize(13);

                    column.Item().PaddingTop(5);
                    column.Item().Text($"{propertyNames[nameof(data.Anamnesis)]}: ").SemiBold().FontSize(11).Bold();
                    column.Item().Text(data.Anamnesis).FontSize(11).LineHeight(1.2f);
                });
        }
    }

    private class ExaminationSection(
        CardiologistReportDataDto data,
        Dictionary<string, string> propertyNames) : IComponent
    {
        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(15)
                .Column(column =>
                {
                    column.Item().Text("Данные первичного обследования").Bold().FontSize(13);
                    column.Item().PaddingTop(5);

                    AddField(column, $"{propertyNames[nameof(data.ElectrocardiographyResult)]}: ", data.ElectrocardiographyResult);
                    AddField(column, $"{propertyNames[nameof(data.EchocardiographyResult)]}: ", data.EchocardiographyResult);
                    AddField(column, $"{propertyNames[nameof(data.SkinState)]}: ", data.SkinState);
                    AddField(column, "Артериальное давление", $"{data.BloodPressureSys}/{data.BloodPressureDia} мм.рт.ст.");
                    AddField(column, $"{propertyNames[nameof(data.HeartRate)]}: ", $"{data.HeartRate} уд/мин");
                    AddField(column, $"{propertyNames[nameof(data.RespiratoryRate)]}: ", $"{data.RespiratoryRate} в мин");
                    AddField(column, $"{propertyNames[nameof(data.BreathingLungs)]}: ", data.BreathingLungs);
                    AddField(column, $"{propertyNames[nameof(data.HasWheeze)]}: ", data.HasWheeze ? "Выявлены" : "Отсутствуют");
                    AddField(column, $"{propertyNames[nameof(data.HasSwellings)]}: ", data.HasSwellings ? "Выявлены" : "Отсутствуют");
                });
        }
    }

    private class DiagnosisSection(
        CardiologistReportDataDto data,
        Dictionary<string, string> propertyNames) : IComponent
    {
        public void Compose(IContainer container)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(15)
                .Column(column =>
                {
                    column.Item().Text("Заключение").Bold().FontSize(13);
                    column.Item().PaddingTop(5);

                    AddTextField(column, $"{propertyNames[nameof(data.Diagnosis)]}: ", data.Diagnosis);
                    AddTextField(column, $"{propertyNames[nameof(data.TreatmentPlan)]}: ", data.TreatmentPlan);
                    AddTextField(column, $"{propertyNames[nameof(data.MedicalExaminationAppointments)]}: ", data.MedicalExaminationAppointments);
                });
        }
    }

    private static void AddField(ColumnDescriptor column, string label, string value)
    {
        column.Item()
            .PaddingVertical(3)
            .Row(row =>
            {
                row.RelativeItem(2)
                    .Text($"{label}")
                    .SemiBold()
                    .FontSize(11);

                row.RelativeItem(3)
                    .Text(value)
                    .FontSize(11);
            });
    }

    private static void AddTextField(ColumnDescriptor column, string label, string value)
    {
        column.Item()
            .PaddingVertical(8)
            .Column(innerColumn =>
            {
                innerColumn.Item()
                    .Text(label)
                    .SemiBold()
                    .FontSize(11);

                innerColumn.Item()
                    .PaddingTop(2)
                    .Text(value)
                    .FontSize(11)
                    .LineHeight(1.3f)
                    .AlignLeft();
            });
    }
}