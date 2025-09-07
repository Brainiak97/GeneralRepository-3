using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RefactorHealthMetric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthMetricsBase");

            migrationBuilder.CreateTable(
                name: "HealthMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: "Наименование показателя (например, \"Уровень стресса\")"),
                    Description = table.Column<string>(type: "text", nullable: true, comment: "Описание показателя (например, \"Оценивайте стресс от 1 до 10\")"),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "Единица измерения (кг., мм.рт.ст., % и т.д.)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetrics", x => x.Id);
                },
                comment: "Показатель здоровья пользователя");

            migrationBuilder.CreateTable(
                name: "HealthMetricsValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    HealthMetricId = table.Column<int>(type: "integer", nullable: false, comment: "Ссылка на созданный пользователем показатель"),
                    Value = table.Column<float>(type: "real", nullable: false, comment: "Значение показателя"),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата и время записи"),
                    Comment = table.Column<string>(type: "text", nullable: true, comment: "Комментарий к записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetricsValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMetricsValue_HealthMetrics_HealthMetricId",
                        column: x => x.HealthMetricId,
                        principalTable: "HealthMetrics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HealthMetricsValue_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Значение показателя здоровья пользователя");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricsValue_HealthMetricId",
                table: "HealthMetricsValue",
                column: "HealthMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricsValue_UserId",
                table: "HealthMetricsValue",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthMetricsValue");

            migrationBuilder.DropTable(
                name: "HealthMetrics");

            migrationBuilder.CreateTable(
                name: "HealthMetricsBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    BloodPressureDia = table.Column<short>(type: "smallint", nullable: true, comment: "Нижнее артериальное давление (мм рт. ст.)"),
                    BloodPressureSys = table.Column<short>(type: "smallint", nullable: true, comment: "Верхнее артериальное давление (мм рт. ст.)"),
                    BodyFatPercentage = table.Column<float>(type: "real", nullable: true, comment: "Процент жира в организме"),
                    HeartRate = table.Column<short>(type: "smallint", nullable: false, comment: "Частота сердечных сокращений (ударов/мин)"),
                    MetricDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата замера показателя"),
                    WaterIntake = table.Column<short>(type: "smallint", nullable: true, comment: "Потребление воды (мл)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetricsBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMetricsBase_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Базовые медицинские показатели");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricsBase_UserId",
                table: "HealthMetricsBase",
                column: "UserId");
        }
    }
}
