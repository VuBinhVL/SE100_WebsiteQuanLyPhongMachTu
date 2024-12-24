using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("ChiTietDonThuocs")]
	public class ChiTietDonThuoc
	{
		[Required]
		public int ChiTietKhamBenhId { get; set; }
		[Required]
		[ForeignKey(nameof(ChiTietKhamBenhId))]
		public ChiTietKhamBenh? ChiTietKhamBenh { get; set; }

		[Required]
		public int ThuocId { get; set; }
		[Required]
		[ForeignKey(nameof(ThuocId))]
		public Thuoc? Thuoc { get; set; }

		[Required]
		public int SoLuong { get; set; }
		[Required]
		public int DonGia { get; set; }
		[MaxLength(1000)]
		public string? GhiChu { get; set; }
	}
}
