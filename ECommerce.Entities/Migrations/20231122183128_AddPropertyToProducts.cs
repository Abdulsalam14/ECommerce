using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Entities.Migrations
{
    public partial class AddPropertyToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAdded",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAdded",
                table: "Products");
        }
    }
}
