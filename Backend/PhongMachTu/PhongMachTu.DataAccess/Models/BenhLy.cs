using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{

	[Table("BenhLys")]
	public class BenhLy
	{
		[Key]
		public int Id {  get; set; }

		[Required]
		[MaxLength(300)]
		public string? TenBenhLy {  get; set; }
		[Required]
		public string? TrieuTrung {  get; set; }
		[Required]
		public int GiaThamKhao {  get; set; }
		[MaxLength(1000)]
		public string? Image {  get; set; }

		[Required]
		public int NhomBenhId {  get; set; }
		[Required]
		[ForeignKey(nameof(NhomBenhId))]
		public NhomBenh ? NhomBenh {  get; set; }
	}
}
