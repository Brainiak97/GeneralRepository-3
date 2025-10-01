using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PolyclinicService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Alter_AppointmentResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentResults_AppointmentSlots_AppointmentSlotId",
                table: "AppointmentResults");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentResults_AppointmentSlotId",
                table: "AppointmentResults");

            migrationBuilder.DropColumn(
                name: "AppointmentSlotId",
                table: "AppointmentResults");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppointmentResults",
                type: "integer",
                nullable: false,
                comment: "Идентификатор результата приёма",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Идентификатор результата приёма")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSlots_AppointmentResults_Id",
                table: "AppointmentSlots",
                column: "Id",
                principalTable: "AppointmentResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSlots_AppointmentResults_Id",
                table: "AppointmentSlots");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppointmentResults",
                type: "integer",
                nullable: false,
                comment: "Идентификатор результата приёма",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Идентификатор результата приёма")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentSlotId",
                table: "AppointmentResults",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Идентификатор слота на приём к врачу из графика");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentResults_AppointmentSlotId",
                table: "AppointmentResults",
                column: "AppointmentSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentResults_AppointmentSlots_AppointmentSlotId",
                table: "AppointmentResults",
                column: "AppointmentSlotId",
                principalTable: "AppointmentSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
