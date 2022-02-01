using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Api.Migrations
{
    public partial class AddTotalOverudeTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalOvedueInDays",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOvedueInDays",
                table: "Users");
        }
    }
}
