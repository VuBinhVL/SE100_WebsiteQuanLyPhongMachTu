using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("ChupChieus")]
	public class ChupChieu
	{
		[Key]
		public int Id {  get; set; }

		[Required]
		[MaxLength(1500)]
		public string ?Images {  get; set; }
		[Required]
		[MaxLength(1500)]
		public string? KetLuan { get; set; }

		[Required]
		public int Gia {  get; set; }

		[Required]
		public int ChiTietKhamBenhId { get; set; }
		[Required]
		[ForeignKey(nameof(ChiTietKhamBenhId))]
		public ChiTietKhamBenh? ChiTietKhamBenh { get; set; }
	}
}
