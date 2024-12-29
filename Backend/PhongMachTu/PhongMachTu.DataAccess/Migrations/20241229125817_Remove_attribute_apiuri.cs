using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class Remove_attribute_apiuri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiUri",
                table: "ChucNangs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiUri",
                table: "ChucNangs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
