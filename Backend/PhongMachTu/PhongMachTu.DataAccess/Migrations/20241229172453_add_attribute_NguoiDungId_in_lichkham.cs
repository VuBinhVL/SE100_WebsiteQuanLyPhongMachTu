using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class add_attribute_NguoiDungId_in_lichkham : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BenhNhanId",
                table: "LichKhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LichKhams_BenhNhanId",
                table: "LichKhams",
                column: "BenhNhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_LichKhams_NguoiDungs_BenhNhanId",
                table: "LichKhams",
                column: "BenhNhanId",
                principalTable: "NguoiDungs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichKhams_NguoiDungs_BenhNhanId",
                table: "LichKhams");

            migrationBuilder.DropIndex(
                name: "IX_LichKhams_BenhNhanId",
                table: "LichKhams");

            migrationBuilder.DropColumn(
                name: "BenhNhanId",
                table: "LichKhams");
        }
    }
}
