using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeveloper.CibertecMvcIdentity.Migrations
{
    public partial class AddDniToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dni",
                table: "AspNetUsers",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dni",
                table: "AspNetUsers");
        }
    }
}
