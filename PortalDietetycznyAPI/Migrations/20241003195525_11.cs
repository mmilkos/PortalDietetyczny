using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDietetycznyAPI.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasInvoice",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Diets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Buyer_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buyer_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buyer_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buyer_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InvoiceId",
                table: "Orders",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diets_InvoiceId",
                table: "Diets",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diets_Invoices_InvoiceId",
                table: "Diets",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Invoices_InvoiceId",
                table: "Orders",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diets_Invoices_InvoiceId",
                table: "Diets");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Invoices_InvoiceId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Orders_InvoiceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Diets_InvoiceId",
                table: "Diets");

            migrationBuilder.DropColumn(
                name: "HasInvoice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Diets");
        }
    }
}
