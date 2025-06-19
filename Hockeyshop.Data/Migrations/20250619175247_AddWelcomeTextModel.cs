using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hockeyshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWelcomeTextModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderIdOrder",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderIdOrder",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderIdOrder",
                table: "OrderItems",
                column: "OrderIdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderIdOrder",
                table: "OrderItems",
                column: "OrderIdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder");
        }
    }
}
