using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDietUserIdAK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Diet_UserId",
                table: "Diet",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Diet_UserId",
                table: "Diet");
        }
    }
}
