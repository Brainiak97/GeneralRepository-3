using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolyclinicService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Alter_AppointmentSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSlots_AppointmentResults_Id",
                table: "AppointmentSlots");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentSlotId",
                table: "AppointmentResults",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Идентификатор слота приёма в графике поликлиники");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentResults_AppointmentSlotId",
                table: "AppointmentResults",
                column: "AppointmentSlotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentResults_AppointmentSlots_AppointmentSlotId",
                table: "AppointmentResults",
                column: "AppointmentSlotId",
                principalTable: "AppointmentSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSlots_AppointmentResults_Id",
                table: "AppointmentSlots",
                column: "Id",
                principalTable: "AppointmentResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
