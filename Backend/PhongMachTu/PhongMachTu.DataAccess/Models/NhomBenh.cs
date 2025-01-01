using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{

	[Table("NhomBenhs")]
	public class NhomBenh
	{
		[Key]
		public int Id {  get; set; }

		[Required]
		[MaxLength(500)]
		public string? TenNhomBenh { get; set; }
		public IEnumerable<BenhLy>? BenhLys {  get; set; }
	}
}
