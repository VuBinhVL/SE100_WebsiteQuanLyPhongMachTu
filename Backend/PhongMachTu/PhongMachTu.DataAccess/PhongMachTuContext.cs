using Microsoft.EntityFrameworkCore;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess
{
	public class PhongMachTuContext : DbContext
	{
		public DbSet<BenhLy>? BenhLys { get; set; }
		public DbSet<CaKham>? CaKhams { get; set; }
		public DbSet<ChiTietDonThuoc>? ChiTietDonThuocs { get; set; }
		public DbSet<ChiTietHoSoBenhAn>? ChiTietHoSoBenhAns { get; set; }
		public DbSet<ChiTietKhamBenh>? ChiTietKhamBenhs { get; set; }
		public DbSet<ChiTietPhieuNhapThuoc>? ChiTietPhieuNhapThuocs { get; set; }
		public DbSet<ChiTietXetNghiem>? ChiTietXetNghiems { get; set; }
		public DbSet<ChucNang>? ChucNangs { get; set; }
		public DbSet<ChupChieu>? ChupChieus { get; set; }
		public DbSet<DonViTinh>? DonViTinhs { get; set; }
		public DbSet<HoSoBenhAn>? HoSoBenhAns { get; set; }
		public DbSet<LichKham>? LichKhams { get; set; }
		public DbSet<LoaiThuoc>? LoaiThuocs { get; set; }
		public DbSet<LoaiXetNghiem>? LoaiXetNghiems { get; set; }
		public DbSet<NguoiDung>? NguoiDungs { get; set; }
		public DbSet<NhomBenh>? NhomBenhs { get; set; }
		public DbSet<PhieuKhamBenh>? PhieuKhamBenhs { get; set; }
		public DbSet<PhieuNhapThuoc>? PhieuNhapThuocs { get; set; }
		public DbSet<SuChoPhep>? SuChoPheps { get; set; }
		public DbSet<Thuoc>? Thuocs { get; set; }
		public DbSet<TrangThaiLichKham>? TrangThaiLichKhams { get; set; }
		public DbSet<VaiTro>? VaiTros { get; set; }
		public DbSet<ThamSo>? ThamSos { get; set; }

		//bổ trợ DI
		public PhongMachTuContext(DbContextOptions<PhongMachTuContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region add default
			modelBuilder.Entity<NguoiDung>()
				.Property(e => e.Image)
				.HasDefaultValue("no_img.png");

			modelBuilder.Entity<Thuoc>()
				.Property(e => e.Images)
				.HasDefaultValue("[\"no_img.png\"]");

			modelBuilder.Entity<BenhLy>()
				.Property(e => e.Images)
				.HasDefaultValue("[\"no_img.png\"]");

			#endregion

			#region không cho phép tự xóa khóa ngoại
			foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
				 .SelectMany(e => e.GetForeignKeys()))
			{
				foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
			}
			#endregion không cho phép tự xóa khóa ngoại

			#region add khóa chính
			modelBuilder.Entity<ChiTietDonThuoc>()
			.HasKey(cd => new { cd.ChiTietKhamBenhId, cd.ThuocId });

			modelBuilder.Entity<ChiTietPhieuNhapThuoc>()
			.HasKey(cd => new { cd.PhieuNhapThuocId, cd.ThuocId });

			modelBuilder.Entity<SuChoPhep>()
			.HasKey(cd => new { cd.ChucNangId, cd.NguoiDungId });

			modelBuilder.Entity<ChiTietHoSoBenhAn>()
			.HasKey(cd => new { cd.HoSoBenhAnId, cd.ChiTietKhamBenhId });

			modelBuilder.Entity<ChiTietXetNghiem>()
			.HasKey(cd => new { cd.ChiTietKhamBenhId, cd.LoaiXetNghiemId });
			#endregion



		}
	}
}
