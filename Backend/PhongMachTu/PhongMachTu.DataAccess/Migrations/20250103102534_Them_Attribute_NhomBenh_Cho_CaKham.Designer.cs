﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhongMachTu.DataAccess;

#nullable disable

namespace PhongMachTu.DataAccess.Migrations
{
    [DbContext(typeof(PhongMachTuContext))]
    [Migration("20250103102534_Them_Attribute_NhomBenh_Cho_CaKham")]
    partial class Them_Attribute_NhomBenh_Cho_CaKham
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.BenhLy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GiaThamKhao")
                        .HasColumnType("int");

                    b.Property<string>("Images")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasDefaultValue("[\"no_img.png\"]");

                    b.Property<int>("NhomBenhId")
                        .HasColumnType("int");

                    b.Property<string>("TenBenhLy")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("TrieuTrung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NhomBenhId");

                    b.ToTable("BenhLys");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.CaKham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BacSiId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayKham")
                        .HasColumnType("datetime2");

                    b.Property<int>("NhomBenhId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongBenhNhanToiDa")
                        .HasColumnType("int");

                    b.Property<string>("TenCaKham")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<TimeSpan>("ThoiGianBatDau")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ThoiGianKetThuc")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("BacSiId");

                    b.HasIndex("NhomBenhId");

                    b.ToTable("CaKhams");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietDonThuoc", b =>
                {
                    b.Property<int>("ChiTietKhamBenhId")
                        .HasColumnType("int");

                    b.Property<int>("ThuocId")
                        .HasColumnType("int");

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("ChiTietKhamBenhId", "ThuocId");

                    b.HasIndex("ThuocId");

                    b.ToTable("ChiTietDonThuocs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietHoSoBenhAn", b =>
                {
                    b.Property<int>("HoSoBenhAnId")
                        .HasColumnType("int");

                    b.Property<int>("ChiTietKhamBenhId")
                        .HasColumnType("int");

                    b.HasKey("HoSoBenhAnId", "ChiTietKhamBenhId");

                    b.HasIndex("ChiTietKhamBenhId");

                    b.ToTable("ChiTietHoSoBenhAns");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BenhLyId")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("GiaKham")
                        .HasColumnType("int");

                    b.Property<int>("PhieuKhamBenhId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BenhLyId");

                    b.HasIndex("PhieuKhamBenhId");

                    b.ToTable("ChiTietKhamBenhs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietPhieuNhapThuoc", b =>
                {
                    b.Property<int>("PhieuNhapThuocId")
                        .HasColumnType("int");

                    b.Property<int>("ThuocId")
                        .HasColumnType("int");

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("PhieuNhapThuocId", "ThuocId");

                    b.HasIndex("ThuocId");

                    b.ToTable("ChiTietPhieuNhapThuocs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietXetNghiem", b =>
                {
                    b.Property<int>("ChiTietKhamBenhId")
                        .HasColumnType("int");

                    b.Property<int>("LoaiXetNghiemId")
                        .HasColumnType("int");

                    b.Property<string>("DanhGia")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("GiaXetNghiem")
                        .HasColumnType("int");

                    b.Property<double>("KetQua")
                        .HasColumnType("float");

                    b.HasKey("ChiTietKhamBenhId", "LoaiXetNghiemId");

                    b.HasIndex("LoaiXetNghiemId");

                    b.ToTable("ChiTietXetNghiems");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChucNang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TenChucNang")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("ChucNangs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChupChieu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChiTietKhamBenhId")
                        .HasColumnType("int");

                    b.Property<int>("Gia")
                        .HasColumnType("int");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("KetLuan")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.HasKey("Id");

                    b.HasIndex("ChiTietKhamBenhId");

                    b.ToTable("ChupChieus");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.DonViTinh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TenDonViTinh")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("DonViTinhs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.HoSoBenhAn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BenhNhanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("NhomBenhId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BenhNhanId");

                    b.HasIndex("NhomBenhId");

                    b.ToTable("HoSoBenhAns");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.LichKham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BenhNhanId")
                        .HasColumnType("int");

                    b.Property<int>("CaKhamId")
                        .HasColumnType("int");

                    b.Property<int>("SoThuTu")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiLichKhamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BenhNhanId");

                    b.HasIndex("CaKhamId");

                    b.HasIndex("TrangThaiLichKhamId");

                    b.ToTable("LichKhams");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.LoaiThuoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TenLoaiThuoc")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("LoaiThuocs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.LoaiXetNghiem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DonViTinhId")
                        .HasColumnType("int");

                    b.Property<int>("GiaThamKhao")
                        .HasColumnType("int");

                    b.Property<string>("TenXetNghiem")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("DonViTinhId");

                    b.ToTable("LoaiXetNghiems");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.NguoiDung", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ChuyenMonId")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Image")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasDefaultValue("no_img.png");

                    b.Property<bool>("IsLock")
                        .HasColumnType("bit");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("VaiTroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChuyenMonId");

                    b.HasIndex("VaiTroId");

                    b.ToTable("NguoiDungs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.NhomBenh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TenNhomBenh")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("NhomBenhs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.PhieuKhamBenh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LichKhamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LichKhamId");

                    b.ToTable("PhieuKhamBenhs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.PhieuNhapThuoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("NgayNhap")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PhieuNhapThuocs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.SuChoPhep", b =>
                {
                    b.Property<int>("ChucNangId")
                        .HasColumnType("int");

                    b.Property<int>("NguoiDungId")
                        .HasColumnType("int");

                    b.HasKey("ChucNangId", "NguoiDungId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("SuChoPheps");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ThamSo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("HeSoBan")
                        .HasColumnType("float");

                    b.Property<int>("SoLanHuyLichKhamToiDaChoPhep")
                        .HasColumnType("int");

                    b.Property<int>("SoPhutNgungDangKyTruocKetThuc")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ThamSos");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.Thuoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GiaNhap")
                        .HasColumnType("int");

                    b.Property<DateTime>("HanSuDung")
                        .HasColumnType("datetime2");

                    b.Property<string>("Images")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasDefaultValue("[\"no_img.png\"]");

                    b.Property<int>("LoaiThuocId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgaySanXuat")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuongTon")
                        .HasColumnType("int");

                    b.Property<string>("TenThuoc")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("LoaiThuocId");

                    b.ToTable("Thuocs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.TrangThaiLichKham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TenTrangThai")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("TrangThaiLichKhams");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.VaiTro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ChucNangIdsDefault")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("TenVaiTro")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("VaiTros");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.BenhLy", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.NhomBenh", "NhomBenh")
                        .WithMany("BenhLys")
                        .HasForeignKey("NhomBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NhomBenh");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.CaKham", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.NguoiDung", "BacSi")
                        .WithMany()
                        .HasForeignKey("BacSiId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PhongMachTu.DataAccess.Models.NhomBenh", "NhomBenh")
                        .WithMany()
                        .HasForeignKey("NhomBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BacSi");

                    b.Navigation("NhomBenh");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietDonThuoc", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", "ChiTietKhamBenh")
                        .WithMany("ChiTietDonThuocs")
                        .HasForeignKey("ChiTietKhamBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.Thuoc", "Thuoc")
                        .WithMany()
                        .HasForeignKey("ThuocId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChiTietKhamBenh");

                    b.Navigation("Thuoc");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietHoSoBenhAn", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", "ChiTietKhamBenh")
                        .WithMany()
                        .HasForeignKey("ChiTietKhamBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.HoSoBenhAn", "HoSoBenhAn")
                        .WithMany("ChiTietHoSoBenhAn")
                        .HasForeignKey("HoSoBenhAnId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChiTietKhamBenh");

                    b.Navigation("HoSoBenhAn");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.BenhLy", "BenhLy")
                        .WithMany()
                        .HasForeignKey("BenhLyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.PhieuKhamBenh", "PhieuKhamBenh")
                        .WithMany("ChiTietKhamBenhs")
                        .HasForeignKey("PhieuKhamBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BenhLy");

                    b.Navigation("PhieuKhamBenh");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietPhieuNhapThuoc", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.PhieuNhapThuoc", "PhieuNhapThuoc")
                        .WithMany("ChiTietPhieuNhapThuocs")
                        .HasForeignKey("PhieuNhapThuocId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.Thuoc", "Thuoc")
                        .WithMany()
                        .HasForeignKey("ThuocId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PhieuNhapThuoc");

                    b.Navigation("Thuoc");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietXetNghiem", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", "ChiTietKhamBenh")
                        .WithMany()
                        .HasForeignKey("ChiTietKhamBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.LoaiXetNghiem", "LoaiXetNghiem")
                        .WithMany()
                        .HasForeignKey("LoaiXetNghiemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChiTietKhamBenh");

                    b.Navigation("LoaiXetNghiem");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChupChieu", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", "ChiTietKhamBenh")
                        .WithMany("ChupChieus")
                        .HasForeignKey("ChiTietKhamBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChiTietKhamBenh");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.HoSoBenhAn", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.NguoiDung", "BenhNhan")
                        .WithMany("HoSoBenhAns")
                        .HasForeignKey("BenhNhanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.NhomBenh", "NhomBenh")
                        .WithMany()
                        .HasForeignKey("NhomBenhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BenhNhan");

                    b.Navigation("NhomBenh");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.LichKham", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.NguoiDung", "BenhNhan")
                        .WithMany()
                        .HasForeignKey("BenhNhanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.CaKham", "CaKham")
                        .WithMany("LichKhams")
                        .HasForeignKey("CaKhamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.TrangThaiLichKham", "TrangThaiLichKham")
                        .WithMany()
                        .HasForeignKey("TrangThaiLichKhamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BenhNhan");

                    b.Navigation("CaKham");

                    b.Navigation("TrangThaiLichKham");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.LoaiXetNghiem", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.DonViTinh", "DonViTinh")
                        .WithMany()
                        .HasForeignKey("DonViTinhId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DonViTinh");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.NguoiDung", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.NhomBenh", "ChuyenMon")
                        .WithMany()
                        .HasForeignKey("ChuyenMonId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PhongMachTu.DataAccess.Models.VaiTro", "VaiTro")
                        .WithMany()
                        .HasForeignKey("VaiTroId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChuyenMon");

                    b.Navigation("VaiTro");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.PhieuKhamBenh", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.LichKham", "LichKham")
                        .WithMany()
                        .HasForeignKey("LichKhamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("LichKham");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.SuChoPhep", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.ChucNang", "ChucNang")
                        .WithMany()
                        .HasForeignKey("ChucNangId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PhongMachTu.DataAccess.Models.NguoiDung", "NguoiDung")
                        .WithMany("SuChoPheps")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChucNang");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.Thuoc", b =>
                {
                    b.HasOne("PhongMachTu.DataAccess.Models.LoaiThuoc", "LoaiThuoc")
                        .WithMany()
                        .HasForeignKey("LoaiThuocId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("LoaiThuoc");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.CaKham", b =>
                {
                    b.Navigation("LichKhams");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.ChiTietKhamBenh", b =>
                {
                    b.Navigation("ChiTietDonThuocs");

                    b.Navigation("ChupChieus");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.HoSoBenhAn", b =>
                {
                    b.Navigation("ChiTietHoSoBenhAn");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.NguoiDung", b =>
                {
                    b.Navigation("HoSoBenhAns");

                    b.Navigation("SuChoPheps");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.NhomBenh", b =>
                {
                    b.Navigation("BenhLys");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.PhieuKhamBenh", b =>
                {
                    b.Navigation("ChiTietKhamBenhs");
                });

            modelBuilder.Entity("PhongMachTu.DataAccess.Models.PhieuNhapThuoc", b =>
                {
                    b.Navigation("ChiTietPhieuNhapThuocs");
                });
#pragma warning restore 612, 618
        }
    }
}
