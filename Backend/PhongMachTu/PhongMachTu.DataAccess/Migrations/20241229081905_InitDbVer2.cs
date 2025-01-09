using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    public partial class InitDbVer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucNangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucNang = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ApiUri = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucNangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonViTinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDonViTinh = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViTinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiThuocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiThuoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThuocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhomBenhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhomBenh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomBenhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhapThuocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhapThuocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiLichKhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiLichKhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ChucNangIdsDefault = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiXetNghiems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenXetNghiem = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    GiaThamKhao = table.Column<int>(type: "int", nullable: false),
                    DonViTinhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiXetNghiems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoaiXetNghiems_DonViTinhs_DonViTinhId",
                        column: x => x.DonViTinhId,
                        principalTable: "DonViTinhs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "Thuocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuoc = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Images = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, defaultValue: "[\"no_img.png\"]"),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<int>(type: "int", nullable: false),
                    NgaySanXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanSuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiThuocId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thuocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thuocs_LoaiThuocs_LoaiThuocId",
                        column: x => x.LoaiThuocId,
                        principalTable: "LoaiThuocs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "BenhLys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBenhLy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TrieuTrung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaThamKhao = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, defaultValue: "[\"no_img.png\"]"),
                    NhomBenhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenhLys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenhLys_NhomBenhs_NhomBenhId",
                        column: x => x.NhomBenhId,
                        principalTable: "NhomBenhs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTaiKhoan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, defaultValue: "no_img.png"),
                    HoTen = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLock = table.Column<bool>(type: "bit", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VaiTroId = table.Column<int>(type: "int", nullable: false),
                    ChuyenMonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDungs_NhomBenhs_ChuyenMonId",
                        column: x => x.ChuyenMonId,
                        principalTable: "NhomBenhs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_NguoiDungs_VaiTros_VaiTroId",
                        column: x => x.VaiTroId,
                        principalTable: "VaiTros",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhapThuocs",
                columns: table => new
                {
                    PhieuNhapThuocId = table.Column<int>(type: "int", nullable: false),
                    ThuocId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhapThuocs", x => new { x.PhieuNhapThuocId, x.ThuocId });
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhapThuocs_PhieuNhapThuocs_PhieuNhapThuocId",
                        column: x => x.PhieuNhapThuocId,
                        principalTable: "PhieuNhapThuocs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhapThuocs_Thuocs_ThuocId",
                        column: x => x.ThuocId,
                        principalTable: "Thuocs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "CaKhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCaKham = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ThoiGianBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiGianKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    NgayKham = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongBenhNhanToiDa = table.Column<int>(type: "int", nullable: false),
                    BacSiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaKhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaKhams_NguoiDungs_BacSiId",
                        column: x => x.BacSiId,
                        principalTable: "NguoiDungs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "HoSoBenhAns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BenhNhanId = table.Column<int>(type: "int", nullable: false),
                    NhomBenhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoBenhAns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoSoBenhAns_NguoiDungs_BenhNhanId",
                        column: x => x.BenhNhanId,
                        principalTable: "NguoiDungs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_HoSoBenhAns_NhomBenhs_NhomBenhId",
                        column: x => x.NhomBenhId,
                        principalTable: "NhomBenhs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "SuChoPheps",
                columns: table => new
                {
                    ChucNangId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuChoPheps", x => new { x.ChucNangId, x.NguoiDungId });
                    table.ForeignKey(
                        name: "FK_SuChoPheps_ChucNangs_ChucNangId",
                        column: x => x.ChucNangId,
                        principalTable: "ChucNangs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_SuChoPheps_NguoiDungs_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDungs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "LichKhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoThuTu = table.Column<int>(type: "int", nullable: false),
                    TrangThaiLichKhamId = table.Column<int>(type: "int", nullable: false),
                    CaKhamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichKhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichKhams_CaKhams_CaKhamId",
                        column: x => x.CaKhamId,
                        principalTable: "CaKhams",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_LichKhams_TrangThaiLichKhams_TrangThaiLichKhamId",
                        column: x => x.TrangThaiLichKhamId,
                        principalTable: "TrangThaiLichKhams",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "PhieuKhamBenhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LichKhamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuKhamBenhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuKhamBenhs_LichKhams_LichKhamId",
                        column: x => x.LichKhamId,
                        principalTable: "LichKhams",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKhamBenhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuKhamBenhId = table.Column<int>(type: "int", nullable: false),
                    BenhLyId = table.Column<int>(type: "int", nullable: false),
                    GiaKham = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietKhamBenhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietKhamBenhs_BenhLys_BenhLyId",
                        column: x => x.BenhLyId,
                        principalTable: "BenhLys",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_ChiTietKhamBenhs_PhieuKhamBenhs_PhieuKhamBenhId",
                        column: x => x.PhieuKhamBenhId,
                        principalTable: "PhieuKhamBenhs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonThuocs",
                columns: table => new
                {
                    ChiTietKhamBenhId = table.Column<int>(type: "int", nullable: false),
                    ThuocId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonThuocs", x => new { x.ChiTietKhamBenhId, x.ThuocId });
                    table.ForeignKey(
                        name: "FK_ChiTietDonThuocs_ChiTietKhamBenhs_ChiTietKhamBenhId",
                        column: x => x.ChiTietKhamBenhId,
                        principalTable: "ChiTietKhamBenhs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_ChiTietDonThuocs_Thuocs_ThuocId",
                        column: x => x.ThuocId,
                        principalTable: "Thuocs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoSoBenhAns",
                columns: table => new
                {
                    HoSoBenhAnId = table.Column<int>(type: "int", nullable: false),
                    ChiTietKhamBenhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoSoBenhAns", x => new { x.HoSoBenhAnId, x.ChiTietKhamBenhId });
                    table.ForeignKey(
                        name: "FK_ChiTietHoSoBenhAns_ChiTietKhamBenhs_ChiTietKhamBenhId",
                        column: x => x.ChiTietKhamBenhId,
                        principalTable: "ChiTietKhamBenhs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_ChiTietHoSoBenhAns_HoSoBenhAns_HoSoBenhAnId",
                        column: x => x.HoSoBenhAnId,
                        principalTable: "HoSoBenhAns",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietXetNghiems",
                columns: table => new
                {
                    ChiTietKhamBenhId = table.Column<int>(type: "int", nullable: false),
                    LoaiXetNghiemId = table.Column<int>(type: "int", nullable: false),
                    KetQua = table.Column<double>(type: "float", nullable: false),
                    DanhGia = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GiaXetNghiem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietXetNghiems", x => new { x.ChiTietKhamBenhId, x.LoaiXetNghiemId });
                    table.ForeignKey(
                        name: "FK_ChiTietXetNghiems_ChiTietKhamBenhs_ChiTietKhamBenhId",
                        column: x => x.ChiTietKhamBenhId,
                        principalTable: "ChiTietKhamBenhs",
                        principalColumn: "ThuocId");
                    table.ForeignKey(
                        name: "FK_ChiTietXetNghiems_LoaiXetNghiems_LoaiXetNghiemId",
                        column: x => x.LoaiXetNghiemId,
                        principalTable: "LoaiXetNghiems",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateTable(
                name: "ChupChieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    KetLuan = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Gia = table.Column<int>(type: "int", nullable: false),
                    ChiTietKhamBenhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChupChieus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChupChieus_ChiTietKhamBenhs_ChiTietKhamBenhId",
                        column: x => x.ChiTietKhamBenhId,
                        principalTable: "ChiTietKhamBenhs",
                        principalColumn: "ThuocId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenhLys_NhomBenhId",
                table: "BenhLys",
                column: "NhomBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_CaKhams_BacSiId",
                table: "CaKhams",
                column: "BacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonThuocs_ThuocId",
                table: "ChiTietDonThuocs",
                column: "ThuocId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoSoBenhAns_ChiTietKhamBenhId",
                table: "ChiTietHoSoBenhAns",
                column: "ChiTietKhamBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhamBenhs_BenhLyId",
                table: "ChiTietKhamBenhs",
                column: "BenhLyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhamBenhs_PhieuKhamBenhId",
                table: "ChiTietKhamBenhs",
                column: "PhieuKhamBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhapThuocs_ThuocId",
                table: "ChiTietPhieuNhapThuocs",
                column: "ThuocId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietXetNghiems_LoaiXetNghiemId",
                table: "ChiTietXetNghiems",
                column: "LoaiXetNghiemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChupChieus_ChiTietKhamBenhId",
                table: "ChupChieus",
                column: "ChiTietKhamBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoBenhAns_BenhNhanId",
                table: "HoSoBenhAns",
                column: "BenhNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoBenhAns_NhomBenhId",
                table: "HoSoBenhAns",
                column: "NhomBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_LichKhams_CaKhamId",
                table: "LichKhams",
                column: "CaKhamId");

            migrationBuilder.CreateIndex(
                name: "IX_LichKhams_TrangThaiLichKhamId",
                table: "LichKhams",
                column: "TrangThaiLichKhamId");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiXetNghiems_DonViTinhId",
                table: "LoaiXetNghiems",
                column: "DonViTinhId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_ChuyenMonId",
                table: "NguoiDungs",
                column: "ChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_VaiTroId",
                table: "NguoiDungs",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKhamBenhs_LichKhamId",
                table: "PhieuKhamBenhs",
                column: "LichKhamId");

            migrationBuilder.CreateIndex(
                name: "IX_SuChoPheps_NguoiDungId",
                table: "SuChoPheps",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_Thuocs_LoaiThuocId",
                table: "Thuocs",
                column: "LoaiThuocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonThuocs");

            migrationBuilder.DropTable(
                name: "ChiTietHoSoBenhAns");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhapThuocs");

            migrationBuilder.DropTable(
                name: "ChiTietXetNghiems");

            migrationBuilder.DropTable(
                name: "ChupChieus");

            migrationBuilder.DropTable(
                name: "SuChoPheps");

            migrationBuilder.DropTable(
                name: "HoSoBenhAns");

            migrationBuilder.DropTable(
                name: "PhieuNhapThuocs");

            migrationBuilder.DropTable(
                name: "Thuocs");

            migrationBuilder.DropTable(
                name: "LoaiXetNghiems");

            migrationBuilder.DropTable(
                name: "ChiTietKhamBenhs");

            migrationBuilder.DropTable(
                name: "ChucNangs");

            migrationBuilder.DropTable(
                name: "LoaiThuocs");

            migrationBuilder.DropTable(
                name: "DonViTinhs");

            migrationBuilder.DropTable(
                name: "BenhLys");

            migrationBuilder.DropTable(
                name: "PhieuKhamBenhs");

            migrationBuilder.DropTable(
                name: "LichKhams");

            migrationBuilder.DropTable(
                name: "CaKhams");

            migrationBuilder.DropTable(
                name: "TrangThaiLichKhams");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "NhomBenhs");

            migrationBuilder.DropTable(
                name: "VaiTros");
        }
    }
}
