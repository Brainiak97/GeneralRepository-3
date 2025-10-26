using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReportService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор отчёта")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Имя файла"),
                    ReportFormat = table.Column<byte>(type: "smallint", nullable: false, comment: "Формат отчёта"),
                    Content = table.Column<byte[]>(type: "bytea", nullable: false, comment: "Содержимое отчёта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                },
                comment: "Отчёты");

            migrationBuilder.CreateTable(
                name: "ReportTemplatesMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор шаблона")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "Имя шаблона"),
                    ReportTemplateTypeName = table.Column<string>(type: "text", nullable: false, comment: "Наименование типа источника данных для шаблона отчёта в приложении")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplatesMetadata", x => x.Id);
                },
                comment: "Метаданные по шаблонам отчётов");

            migrationBuilder.InsertData(
                table: "ReportTemplatesMetadata",
                columns: new[] { "Id", "Name", "ReportTemplateTypeName"},
                values: new object[,]
                {
                    { 1, "Cтандартный отчёт", "DefaultReportTemplate"},
                    { 2, "Отчёт кардиолога", "CardiologistReportTemplate"},
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ReportTemplatesMetadata");
        }
    }
}
