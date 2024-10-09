using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDietetycznyAPI.Migrations
{
    /// <inheritdoc />
    public partial class _15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diets_Orders_OrderId",
                table: "Diets");

            migrationBuilder.DropIndex(
                name: "IX_Diets_OrderId",
                table: "Diets");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Diets");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_OrderId",
                table: "Files",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Orders_OrderId",
                table: "Files",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Orders_OrderId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_OrderId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Diets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diets_OrderId",
                table: "Diets",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diets_Orders_OrderId",
                table: "Diets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
