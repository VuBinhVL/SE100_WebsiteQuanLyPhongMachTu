using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class remove_attribute_BenhLyId_cua_ChiTietHoSoBenhAn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoSoBenhAns_BenhLys_BenhLyId",
                table: "ChiTietHoSoBenhAns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietHoSoBenhAns",
                table: "ChiTietHoSoBenhAns");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoSoBenhAns_BenhLyId",
                table: "ChiTietHoSoBenhAns");

            migrationBuilder.DropColumn(
                name: "BenhLyId",
                table: "ChiTietHoSoBenhAns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietHoSoBenhAns",
                table: "ChiTietHoSoBenhAns",
                columns: new[] { "HoSoBenhAnId", "ChiTietKhamBenhId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietHoSoBenhAns",
                table: "ChiTietHoSoBenhAns");

            migrationBuilder.AddColumn<int>(
                name: "BenhLyId",
                table: "ChiTietHoSoBenhAns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietHoSoBenhAns",
                table: "ChiTietHoSoBenhAns",
                columns: new[] { "HoSoBenhAnId", "BenhLyId" });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoSoBenhAns_BenhLyId",
                table: "ChiTietHoSoBenhAns",
                column: "BenhLyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoSoBenhAns_BenhLys_BenhLyId",
                table: "ChiTietHoSoBenhAns",
                column: "BenhLyId",
                principalTable: "BenhLys",
                principalColumn: "Id");
        }
    }
}
