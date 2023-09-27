using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigOn_WebUI.Migrations
{
    public partial class BigonUserEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                schema: "Membership",
                table: "Users");
        }
    }
}
