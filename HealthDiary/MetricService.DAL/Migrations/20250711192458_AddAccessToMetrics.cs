using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessToMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessToMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderUserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя, который предоставляет доступ"),
                    GrantedUserId = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор пользователя, которому предоставляется доступ"),
                    AccessExpirationDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата, до которой действует доступ"),
                    IsPermanentAccess = table.Column<bool>(type: "boolean", nullable: false, comment: "Доступ без ограничения по срокам")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessToMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessToMetrics_Users_GrantedUserId",
                        column: x => x.GrantedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessToMetrics_Users_ProviderUserId",
                        column: x => x.ProviderUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Доступ к личным метрикам");

            migrationBuilder.CreateIndex(
                name: "IX_AccessToMetrics_GrantedUserId",
                table: "AccessToMetrics",
                column: "GrantedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessToMetrics_ProviderUserId",
                table: "AccessToMetrics",
                column: "ProviderUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessToMetrics");
        }
    }
}
