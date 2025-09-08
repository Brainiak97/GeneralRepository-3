using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthMetricInitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HealthMetrics",
                columns: new[] { "Id", "Description", "Name", "Unit" },
                values: new object[,]
                {
                    { 1, "Пульс", "Частота сердечных сокращений", "ударов/мин" },
                    { 2, "Верхнее давление", "Верхнее артериальное давление", "мм рт. ст." },
                    { 3, "Нижнее давление", "Нижнее артериальное давление", "мм рт. ст." },
                    { 4, "Процент жира в организме", "Процент жира в организме", "%" },
                    { 5, "Суточное потребление воды", "Потребление воды", "мл" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HealthMetrics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HealthMetrics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HealthMetrics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HealthMetrics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HealthMetrics",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
