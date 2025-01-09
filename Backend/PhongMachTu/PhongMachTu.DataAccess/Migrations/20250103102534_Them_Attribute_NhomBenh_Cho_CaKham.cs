using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class Them_Attribute_NhomBenh_Cho_CaKham : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhomBenhId",
                table: "CaKhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CaKhams_NhomBenhId",
                table: "CaKhams",
                column: "NhomBenhId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaKhams_NhomBenhs_NhomBenhId",
                table: "CaKhams",
                column: "NhomBenhId",
                principalTable: "NhomBenhs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaKhams_NhomBenhs_NhomBenhId",
                table: "CaKhams");

            migrationBuilder.DropIndex(
                name: "IX_CaKhams_NhomBenhId",
                table: "CaKhams");

            migrationBuilder.DropColumn(
                name: "NhomBenhId",
                table: "CaKhams");
        }
    }
}
