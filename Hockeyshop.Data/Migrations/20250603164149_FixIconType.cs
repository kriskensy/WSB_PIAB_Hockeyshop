using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hockeyshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixIconType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IconLibraries",
                columns: table => new
                {
                    IdIcon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IconLibraries", x => x.IdIcon);
                });

            migrationBuilder.CreateTable(
                name: "ShopRules",
                columns: table => new
                {
                    IdShopRule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdIcon = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconLibraryIdIcon = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopRules", x => x.IdShopRule);
                    table.ForeignKey(
                        name: "FK_ShopRules_IconLibraries_IconLibraryIdIcon",
                        column: x => x.IconLibraryIdIcon,
                        principalTable: "IconLibraries",
                        principalColumn: "IdIcon");
                    table.ForeignKey(
                        name: "FK_ShopRules_IconLibraries_IdIcon",
                        column: x => x.IdIcon,
                        principalTable: "IconLibraries",
                        principalColumn: "IdIcon");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopRules_IconLibraryIdIcon",
                table: "ShopRules",
                column: "IconLibraryIdIcon");

            migrationBuilder.CreateIndex(
                name: "IX_ShopRules_IdIcon",
                table: "ShopRules",
                column: "IdIcon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopRules");

            migrationBuilder.DropTable(
                name: "IconLibraries");
        }
    }
}
