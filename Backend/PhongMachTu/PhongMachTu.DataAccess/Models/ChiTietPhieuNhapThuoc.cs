using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("ChiTietPhieuNhapThuocs")]
	public class ChiTietPhieuNhapThuoc
	{
		[Required]
		public int PhieuNhapThuocId { get; set; }
		[Required]
		[ForeignKey(nameof(PhieuNhapThuocId))]
		public PhieuNhapThuoc? PhieuNhapThuoc { get; set; }

		[Required]
		public int ThuocId { get; set; }
		[Required]
		[ForeignKey(nameof(ThuocId))]
		public Thuoc? Thuoc { get; set; }

		[Required]
		public int SoLuong {  get; set; }
		[Required]
		public int DonGia {  get; set; }
		[MaxLength(1000)]
		public string? GhiChu {  get; set; }
	}
}
