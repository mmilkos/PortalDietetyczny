using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDietetycznyAPI.Migrations
{
    /// <inheritdoc />
    public partial class _16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Buyer_Nip",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceId",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Seller_Nip",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diets_Orders_OrderId",
                table: "Diets");

            migrationBuilder.DropIndex(
                name: "IX_Diets_OrderId",
                table: "Diets");

            migrationBuilder.DropColumn(
                name: "Buyer_Nip",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Seller_Nip",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Diets");
        }
    }
}
