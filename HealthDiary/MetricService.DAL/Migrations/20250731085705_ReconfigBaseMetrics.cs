using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReconfigBaseMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "PhysicalActivities",
                comment: "Физическая активность",
                oldComment: "Тренировки");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                comment: "Идентификатор",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Идентификатор")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "PhysicalActivities",
                comment: "Тренировки",
                oldComment: "Физическая активность");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                comment: "Идентификатор",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Идентификатор")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
