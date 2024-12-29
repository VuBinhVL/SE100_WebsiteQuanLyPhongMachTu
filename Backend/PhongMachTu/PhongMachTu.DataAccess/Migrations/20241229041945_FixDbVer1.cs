using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class FixDbVer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlsDefault",
                table: "VaiTros");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ChucNangs",
                newName: "ApiUri");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySanXuat",
                table: "Thuocs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgaySanXuat",
                table: "Thuocs");

            migrationBuilder.RenameColumn(
                name: "ApiUri",
                table: "ChucNangs",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "UrlsDefault",
                table: "VaiTros",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
