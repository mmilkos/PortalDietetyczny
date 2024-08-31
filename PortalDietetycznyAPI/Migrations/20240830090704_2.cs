using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDietetycznyAPI.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Uid",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Uid",
                table: "Recipes",
                column: "Uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_Uid",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Recipes");
        }
    }
}
