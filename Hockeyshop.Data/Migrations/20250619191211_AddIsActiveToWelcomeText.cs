using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hockeyshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToWelcomeText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WelcomeTexts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WelcomeTexts");
        }
    }
}
