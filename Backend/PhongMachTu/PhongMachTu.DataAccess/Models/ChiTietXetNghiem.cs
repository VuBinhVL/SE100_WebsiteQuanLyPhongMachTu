using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("ChiTietXetNghiems")]
	public class ChiTietXetNghiem
	{
		[Required]
		public int ChiTietKhamBenhId { get; set; }
		[Required]
		[ForeignKey(nameof(ChiTietKhamBenhId))]
		public ChiTietKhamBenh? ChiTietKhamBenh { get; set; }

		[Required]
		public int LoaiXetNghiemId { get; set; }
		[Required]
		[ForeignKey(nameof(LoaiXetNghiemId))]
		public LoaiXetNghiem? LoaiXetNghiem { get; set; }

		[Required]
		public double KetQua {  get; set; }

		[MaxLength(1000)]
		public string? DanhGia {  get; set; }

		[Required]
		public int GiaXetNghiem { get; set; }
	}
}
