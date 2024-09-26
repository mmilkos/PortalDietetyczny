using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDietetycznyAPI.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Diets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Diets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
