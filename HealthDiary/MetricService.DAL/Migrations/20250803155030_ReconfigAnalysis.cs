using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetricService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReconfigAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalysisResults_Users_UserId",
                table: "AnalysisResults");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalysisResults_Users_UserId",
                table: "AnalysisResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalysisResults_Users_UserId",
                table: "AnalysisResults");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalysisResults_Users_UserId",
                table: "AnalysisResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
