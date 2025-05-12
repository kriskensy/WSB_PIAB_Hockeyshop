using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hockeyshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_IdProduct",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_UserCarts_IdUserCart",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_IdOrder",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_IdProduct",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_IdUser",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_IdOrder",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_IdPaymentMethod",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentStatuses_IdPaymentStatus",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Products_IdProduct",
                table: "ProductPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Promotions_IdPromotion",
                table: "ProductPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_IdProductCategory",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_IdSupplier",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_IdProduct",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCarts_Users_IdUser",
                table: "UserCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_IdUserRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdOrder",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_IdOrder",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleIdUserRole",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserIdUser",
                table: "UserCarts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductIdProduct",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryIdProductCategory",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierIdSupplier",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIdOrder",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodIdPaymentMethod",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatusIdPaymentStatus",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusIdOrderStatus",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserIdUser",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIdOrder",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductIdProduct",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductIdProduct",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCartIdUserCart",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleIdUserRole",
                table: "Users",
                column: "UserRoleIdUserRole");

            migrationBuilder.CreateIndex(
                name: "IX_UserCarts_UserIdUser",
                table: "UserCarts",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductIdProduct",
                table: "Stocks",
                column: "ProductIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryIdProductCategory",
                table: "Products",
                column: "ProductCategoryIdProductCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierIdSupplier",
                table: "Products",
                column: "SupplierIdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdOrder",
                table: "Payments",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderIdOrder",
                table: "Payments",
                column: "OrderIdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodIdPaymentMethod",
                table: "Payments",
                column: "PaymentMethodIdPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatusIdPaymentStatus",
                table: "Payments",
                column: "PaymentStatusIdPaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusIdOrderStatus",
                table: "Orders",
                column: "OrderStatusIdOrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserIdUser",
                table: "Orders",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderIdOrder",
                table: "OrderItems",
                column: "OrderIdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductIdProduct",
                table: "OrderItems",
                column: "ProductIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IdOrder",
                table: "Invoices",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductIdProduct",
                table: "CartItems",
                column: "ProductIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserCartIdUserCart",
                table: "CartItems",
                column: "UserCartIdUserCart");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_IdProduct",
                table: "CartItems",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductIdProduct",
                table: "CartItems",
                column: "ProductIdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_UserCarts_IdUserCart",
                table: "CartItems",
                column: "IdUserCart",
                principalTable: "UserCarts",
                principalColumn: "IdUserCart");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_UserCarts_UserCartIdUserCart",
                table: "CartItems",
                column: "UserCartIdUserCart",
                principalTable: "UserCarts",
                principalColumn: "IdUserCart");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_IdOrder",
                table: "OrderItems",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderIdOrder",
                table: "OrderItems",
                column: "OrderIdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_IdProduct",
                table: "OrderItems",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductIdProduct",
                table: "OrderItems",
                column: "ProductIdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusIdOrderStatus",
                table: "Orders",
                column: "OrderStatusIdOrderStatus",
                principalTable: "OrderStatuses",
                principalColumn: "IdOrderStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_IdUser",
                table: "Orders",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserIdUser",
                table: "Orders",
                column: "UserIdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_IdOrder",
                table: "Payments",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderIdOrder",
                table: "Payments",
                column: "OrderIdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_IdPaymentMethod",
                table: "Payments",
                column: "IdPaymentMethod",
                principalTable: "PaymentMethods",
                principalColumn: "IdPaymentMethod");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodIdPaymentMethod",
                table: "Payments",
                column: "PaymentMethodIdPaymentMethod",
                principalTable: "PaymentMethods",
                principalColumn: "IdPaymentMethod");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentStatuses_IdPaymentStatus",
                table: "Payments",
                column: "IdPaymentStatus",
                principalTable: "PaymentStatuses",
                principalColumn: "IdPaymentStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentStatuses_PaymentStatusIdPaymentStatus",
                table: "Payments",
                column: "PaymentStatusIdPaymentStatus",
                principalTable: "PaymentStatuses",
                principalColumn: "IdPaymentStatus");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_IdProductCategory",
                table: "Products",
                column: "IdProductCategory",
                principalTable: "ProductCategories",
                principalColumn: "IdProductCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryIdProductCategory",
                table: "Products",
                column: "ProductCategoryIdProductCategory",
                principalTable: "ProductCategories",
                principalColumn: "IdProductCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_IdSupplier",
                table: "Products",
                column: "IdSupplier",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierIdSupplier",
                table: "Products",
                column: "SupplierIdSupplier",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_IdProduct",
                table: "Stocks",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductIdProduct",
                table: "Stocks",
                column: "ProductIdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarts_Users_IdUser",
                table: "UserCarts",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarts_Users_UserIdUser",
                table: "UserCarts",
                column: "UserIdUser",
                principalTable: "Users",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_IdUserRole",
                table: "Users",
                column: "IdUserRole",
                principalTable: "UserRoles",
                principalColumn: "IdUserRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleIdUserRole",
                table: "Users",
                column: "UserRoleIdUserRole",
                principalTable: "UserRoles",
                principalColumn: "IdUserRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_IdProduct",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductIdProduct",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_UserCarts_IdUserCart",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_UserCarts_UserCartIdUserCart",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_IdOrder",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_IdProduct",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductIdProduct",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusIdOrderStatus",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_IdUser",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserIdUser",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_IdOrder",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderIdOrder",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_IdPaymentMethod",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentMethods_PaymentMethodIdPaymentMethod",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentStatuses_IdPaymentStatus",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentStatuses_PaymentStatusIdPaymentStatus",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Products_IdProduct",
                table: "ProductPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPromotions_Promotions_IdPromotion",
                table: "ProductPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_IdProductCategory",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryIdProductCategory",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_IdSupplier",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierIdSupplier",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_IdProduct",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductIdProduct",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCarts_Users_IdUser",
                table: "UserCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCarts_Users_UserIdUser",
                table: "UserCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_IdUserRole",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleIdUserRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRoleIdUserRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserCarts_UserIdUser",
                table: "UserCarts");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductIdProduct",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryIdProductCategory",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierIdSupplier",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdOrder",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderIdOrder",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentMethodIdPaymentMethod",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentStatusIdPaymentStatus",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusIdOrderStatus",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserIdUser",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductIdProduct",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_IdOrder",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductIdProduct",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserCartIdUserCart",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserRoleIdUserRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserIdUser",
                table: "UserCarts");

            migrationBuilder.DropColumn(
                name: "ProductIdProduct",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ProductCategoryIdProductCategory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierIdSupplier",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderIdOrder",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentMethodIdPaymentMethod",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentStatusIdPaymentStatus",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OrderStatusIdOrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserIdUser",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductIdProduct",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductIdProduct",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserCartIdUserCart",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdOrder",
                table: "Payments",
                column: "IdOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IdOrder",
                table: "Invoices",
                column: "IdOrder",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_IdProduct",
                table: "CartItems",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_UserCarts_IdUserCart",
                table: "CartItems",
                column: "IdUserCart",
                principalTable: "UserCarts",
                principalColumn: "IdUserCart",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_IdOrder",
                table: "OrderItems",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_IdProduct",
                table: "OrderItems",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_IdUser",
                table: "Orders",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_IdOrder",
                table: "Payments",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentMethods_IdPaymentMethod",
                table: "Payments",
                column: "IdPaymentMethod",
                principalTable: "PaymentMethods",
                principalColumn: "IdPaymentMethod",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentStatuses_IdPaymentStatus",
                table: "Payments",
                column: "IdPaymentStatus",
                principalTable: "PaymentStatuses",
                principalColumn: "IdPaymentStatus",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_IdProductCategory",
                table: "Products",
                column: "IdProductCategory",
                principalTable: "ProductCategories",
                principalColumn: "IdProductCategory",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_IdSupplier",
                table: "Products",
                column: "IdSupplier",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_IdProduct",
                table: "Stocks",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarts_Users_IdUser",
                table: "UserCarts",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_IdUserRole",
                table: "Users",
                column: "IdUserRole",
                principalTable: "UserRoles",
                principalColumn: "IdUserRole",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
