using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Migrations
{
    public partial class ChangeTextToDescriptiptionInRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Recipe");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipe",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipe");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
