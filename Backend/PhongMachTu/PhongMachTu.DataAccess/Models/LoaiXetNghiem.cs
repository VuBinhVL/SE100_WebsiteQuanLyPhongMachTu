using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("LoaiXetNghiems")]
	public class LoaiXetNghiem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(300)]
		public string? TenXetNghiem { get; set; }

		[Required]
		public int GiaThamKhao {  get; set; }

		[Required]
		public int DonViTinhId {  get; set; }
		[Required]
		[ForeignKey(nameof(DonViTinhId))]
		public DonViTinh ?DonViTinh { get; set; }
	}
}
