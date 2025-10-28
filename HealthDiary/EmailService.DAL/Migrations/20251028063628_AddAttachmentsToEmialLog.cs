using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentsToEmialLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentFileName",
                table: "EmailLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAttachment",
                table: "EmailLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentFileName",
                table: "EmailLogs");

            migrationBuilder.DropColumn(
                name: "HasAttachment",
                table: "EmailLogs");
        }
    }
}
