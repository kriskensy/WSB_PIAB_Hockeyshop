using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hockeyshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameSupplierIdToIdSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Products",
                newName: "IdSupplier");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                newName: "IX_Products_IdSupplier");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_IdSupplier",
                table: "Products",
                column: "IdSupplier",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_IdSupplier",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "IdSupplier",
                table: "Products",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_IdSupplier",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
