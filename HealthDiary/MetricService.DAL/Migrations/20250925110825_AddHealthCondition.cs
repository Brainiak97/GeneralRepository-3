using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя"),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата и время записи"),
                    EmotionalState = table.Column<int>(type: "integer", nullable: false, comment: "Эмоциональное состояние"),
                    PhysicalState = table.Column<int>(type: "integer", nullable: false, comment: "Физическое состояние"),
                    Symptoms = table.Column<string>(type: "text", nullable: true, comment: "Симптомы"),
                    Notes = table.Column<string>(type: "text", nullable: true, comment: "Дополнительные заметки")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthConditions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "состояние здоровья пользователя");

            migrationBuilder.CreateIndex(
                name: "IX_HealthConditions_UserId",
                table: "HealthConditions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthConditions");
        }
    }
}
