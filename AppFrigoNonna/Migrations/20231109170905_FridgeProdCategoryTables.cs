using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppFrigoNonna.Migrations
{
    public partial class FridgeProdCategoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FridgeProds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Freezer = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeProds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFridgeProd",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    FridgeProdsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFridgeProd", x => new { x.CategoriesId, x.FridgeProdsId });
                    table.ForeignKey(
                        name: "FK_CategoryFridgeProd_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFridgeProd_FridgeProds_FridgeProdsId",
                        column: x => x.FridgeProdsId,
                        principalTable: "FridgeProds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFridgeProd_FridgeProdsId",
                table: "CategoryFridgeProd",
                column: "FridgeProdsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryFridgeProd");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FridgeProds");
        }
    }
}
