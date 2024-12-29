using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class FixDBVer2_ThemKeyChoBang_ThamSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThamSos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLanHuyLichKhamToiDaChoPhep = table.Column<int>(type: "int", nullable: false),
                    HeSoBan = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamSos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThamSos");
        }
    }
}
