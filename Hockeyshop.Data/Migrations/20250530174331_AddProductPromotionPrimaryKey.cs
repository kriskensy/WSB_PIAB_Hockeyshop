using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hockeyshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPromotionPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Products_IdProduct",
                table: "ProductPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Promotions_IdPromotion",
                table: "ProductPromotions");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPromotions_Products_IdProduct",
                table: "ProductPromotions",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPromotions_Promotions_IdPromotion",
                table: "ProductPromotions",
                column: "IdPromotion",
                principalTable: "Promotions",
                principalColumn: "IdPromotion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Products_IdProduct",
                table: "ProductPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Promotions_IdPromotion",
                table: "ProductPromotions");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPromotions_Products_IdProduct",
                table: "ProductPromotions",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPromotions_Promotions_IdPromotion",
                table: "ProductPromotions",
                column: "IdPromotion",
                principalTable: "Promotions",
                principalColumn: "IdPromotion");
        }
    }
}
