using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppFrigoNonna.Migrations
{
    public partial class UpdateProdLocationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Freezer",
                table: "FridgeProds");

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "FridgeProds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "FridgeProds");

            migrationBuilder.AddColumn<bool>(
                name: "Freezer",
                table: "FridgeProds",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
